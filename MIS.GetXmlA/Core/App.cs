using MIS_MTR_RH_RI_RE.GetXmlA.Core;
using MIS_MTR_RH_RI_RE.GetXmlA.Models.I;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Channels;


public class App
{
    private static Channel<MisWorkItem> _misChannel = Channel.CreateUnbounded<MisWorkItem>();
    private static Channel<MtrWorkItem> _mtrChannel = Channel.CreateUnbounded<MtrWorkItem>();
    // Количество одновременных обработчиков
    private static int CONSUMER_COUNT = int.TryParse(RepositorySettings.GetSection("CONSUMER_COUNT"), out var parsed) ? parsed : 4;
    private static readonly ProcessingStats Stats = new ProcessingStats();

    public App()
    {
        //Дополнительно: Ограниченный канал (на случай большого объема)
        //Если боитесь перегрузить память        //
        Channel.CreateBounded<MisWorkItem>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait
        });
    }

    public static async Task RunAsync()
    {
        // Запускаем обработчиков (фоновые задачи)
        var consumers = StartConsumers();

        int startYear = int.Parse(RepositorySettings.GetSection("START_YEAR_PERIOD"));
        int startMonth = int.Parse(RepositorySettings.GetSection("START_MONTH_PERIOD"));
        int totalMonths = int.Parse(RepositorySettings.GetSection("COUNT_MONTH_FROM_START"));

        // Список всех задач Execute
        var executeTasks = new List<Task>();

        for (int i = 0; i <= totalMonths; i++)
        {
            int currentYear = startYear;
            int currentMonth = startMonth + i;

            // Коррекция года при переполнении месяцев
            if (currentMonth > 12)
            {
                currentYear += (currentMonth - 1) / 12;
                currentMonth = (currentMonth - 1) % 12 + 1;
            }

            string fileNameXml = $"RD07_{currentYear - 2000:D2}{currentMonth:D2}";

            Console.WriteLine("\n📊 период: {0}-{1:D2} {2}", currentYear, currentMonth, fileNameXml);

            Init.YEAR_REPORT = currentYear;
            Init.MONTH_REPORT = currentMonth;

            executeTasks.Add(new App().ExecuteAsync(currentYear, currentMonth, fileNameXml));
        }

        // 🔥 Ждём, пока ВСЕ ExecuteAsync завершат запись в каналы
        await Task.WhenAll(executeTasks);

        // ✅ Только теперь — закрываем каналы
        _misChannel.Writer.Complete();
        _mtrChannel.Writer.Complete();

        // 🔥 Ждём, пока ВСЕ консьюмеры завершат обработку
        await Task.WhenAll(consumers);

        // ✅ Вывод статистики
        Stats.Print();

        //message
        MessageHelper.Print("Программа выполнена успешно", ConsoleColor.Green);
    }

    private static Task[] StartConsumers()
    {
        var tasks = new Task[CONSUMER_COUNT * 2]; // MIS + MTR

        for (int i = 0; i < CONSUMER_COUNT; i++)
        {
            tasks[i] = Task.Run(() => ConsumeMisAsync());
            tasks[i + CONSUMER_COUNT] = Task.Run(() => ConsumeMtrAsync());
        }

        return tasks;
    }

    private async Task ExecuteAsync(int YEAR, int MONTH, string FileNameXml)
    {
        List<Task> tasks = new List<Task>();

        //---MIS---

        //выбираем пакеты из счетов за период
        if (Init.GET_FROM_MISDB == 1 && (Init.TYPE_OUT_XML_RH == 1 || Init.TYPE_OUT_XML_RHE == 1 || Init.GET_EMPTY_FROM_MISDB == 1 || Init.GET_FROM_MISDB_IDENT == 1))
        {
            IEnumerable<Schet>? H_schets = await new RepositoryMIS(new MisContext()).GetSchets(YEAR, MONTH).ConfigureAwait(false);

            if (H_schets != null && H_schets.Any())
            {
                foreach (var schet in H_schets)
                {
                    if (schet.Id <= 0) continue;

                    string packetFileName = $"{FileNameXml}{schet.YEAR_REPORT}{Initialization.PACKET_MIS_NUM_START}{schet.MONTH_REPORT}{schet.Id}";

                    await _misChannel.Writer.WriteAsync(new MisWorkItem(schet, packetFileName));

                    Stats.SubmitMis();  // ✅ Счётчик отправленных
                }
            }
            else if (Init.TYPE_OUT_XML_RH == 1 || Init.TYPE_OUT_XML_RHE == 1)
            {
                Console.WriteLine("\nв заданном периоде не найден(ы) счет(а) {0} из MISDB\n", 
                       Init.SCHET_NSCHET_MIS?.Count > 0 ? string.Concat("номер ", Init.SCHET_NSCHET_MIS.ToString()) : "");
            }
        }
           

        //---MTR---

        //выбираем пакеты из счетов за период
        if (Init.GET_FROM_MTRDB == 1 && (Init.TYPE_OUT_XML_RI == 1 || Init.TYPE_OUT_XML_RIE == 1 || Init.GET_EMPTY_FROM_MTRDB == 1))
        {
            IEnumerable<Schet_mtr>? H_schets_mtr = await new RepositoryMTR(new MtrContext()).GetSchets(YEAR, MONTH).ConfigureAwait(false);

            if (H_schets_mtr != null && H_schets_mtr.Any())
            {
                foreach (var schet in H_schets_mtr)
                {
                    if (schet.Id <= 0) continue;

                    string packetFileName = $"{FileNameXml}{schet.YEAR}{Initialization.PACKET_MTR_NUM_START}{schet.MONTH}{schet.Id}";

                    await _mtrChannel.Writer.WriteAsync(new MtrWorkItem(schet, packetFileName));

                    Stats.SubmitMtr();  // ✅ Счётчик отправленных
                }
            }
            else if (Init.TYPE_OUT_XML_RI == 1 || Init.TYPE_OUT_XML_RIE == 1)
            {
                Console.WriteLine("\nв заданном периоде не найден(ы) счет(а) {0} из MTRDB\n", 
                    Init.SCHET_NSCHET_MTR?.Count() > 0 ? string.Concat("номер ", Init.SCHET_NSCHET_MTR) : "");
            }
        }

        if (tasks.Count > 0)
            await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    private static async Task ConsumeMisAsync()
    {
        await foreach (var workItem in _misChannel.Reader.ReadAllAsync())
        {
            try
            {
                // Замер времени + автоматическая передача в статистику
                using (var tracker = StopwatchTracker.StartNew(Stats.AddProcessingTime))
                {
                    Console.WriteLine($"MIS Пакет \"{workItem.Schet.FILENAME}\" начал обработку...");

                    await Task.Run(() => CookingMis.Run(workItem.Schet, workItem.FileName)).ConfigureAwait(false);

                    Stats.SuccessMis();  // ✅ Успешно обработан

                    MessageHelper.Print($"✅ MIS Пакет \"{workItem.Schet.FILENAME}\" готов. Время: {tracker._stopwatch.Elapsed:hh\\:mm\\:ss\\.ff}", ConsoleColor.DarkGreen);
                }                    
            }
            catch (Exception ex)
            {
                Stats.ErrorMis();  // ✅ Ошибка

                MessageHelper.Print($"❌ Ошибка при обработке MIS пакета {workItem.Schet.Id}: {ex.Message}.", ConsoleColor.Red);
            }
        }
    }    

    private static async Task ConsumeMtrAsync()
    {
        await foreach (var workItem in _mtrChannel.Reader.ReadAllAsync())
        {
            try
            {
                // Замер времени + автоматическая передача в статистику
                using (var tracker = StopwatchTracker.StartNew(Stats.AddProcessingTime))
                {
                    Console.WriteLine($"MTR Пакет \"{workItem.Schet.FILENAME}\" начал обработку...");

                    await Task.Run(() => CookingMtr.Run(workItem.Schet, workItem.FileName)).ConfigureAwait(false);

                    Stats.SuccessMtr();  // ✅ Успешно обработан

                    MessageHelper.Print($"✅ MTR Пакет \"{workItem.Schet.FILENAME}\" готов. Время: {tracker._stopwatch.Elapsed:hh\\:mm\\:ss\\.ff}", ConsoleColor.DarkGreen);
                }                    
            }
            catch (Exception ex)
            {
                Stats.ErrorMtr();  // ✅ Ошибка

                MessageHelper.Print($"❌ Ошибка при обработке MTR пакета {workItem.Schet.Id}: {ex.Message}.", ConsoleColor.Red);
           }
        }
    }
}