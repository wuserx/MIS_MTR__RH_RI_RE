using MIS_MTR_RH_RI_RE.GetXmlA.Core;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;


public class App
{

    public App()
    {
        
    }

    public static void Run()
    {

        int YEAR = int.Parse(RepositorySettings.GetSection("START_YEAR_PERIOD"));
        int MONTH = int.Parse(RepositorySettings.GetSection("START_MONTH_PERIOD"));
        string MO_CODE = RepositorySettings.GetSection("MO_CODE");
        int COUNT_ZAP2XML = int.Parse(RepositorySettings.GetSection("COUNT_ZAP2XML"));
        decimal COUNT_MONTH_FROM_START = decimal.Parse(RepositorySettings.GetSection("COUNT_MONTH_FROM_START"));
        int COUNT_MONTH = int.Parse(RepositorySettings.GetSection("COUNT_MONTH_FROM_START"));
        int COUNT_YEAR = (int)Math.Ceiling( ((decimal)COUNT_MONTH + MONTH) / 12);
        string FileNameXml = string.Concat("RD07_", YEAR-2000,MONTH < 10 ? "0" + MONTH.ToString() : MONTH);        


        for (int i = 0; i < COUNT_YEAR; i++)
        {
            for (int j = 0; j < (COUNT_MONTH + 1); j++)
            {
                if (MONTH + j > 12)
                {
                    COUNT_MONTH = Math.Abs(j - COUNT_MONTH);
                    MONTH = 1;

                    break;
                }

                Console.WriteLine("\n период: {0}-{1}", YEAR + i, MONTH + j < 10 ? "0" + (MONTH + j).ToString(): MONTH + j);

                FileNameXml = string.Concat("RD07_", (YEAR - 2000) + i, MONTH +j < 10 ? "0" + (MONTH + j).ToString() : MONTH + j);

                Init.YEAR_REPORT = YEAR + i;

                Init.MONTH_REPORT = MONTH + j;

                //Console.WriteLine(FileNameXml);
                new App().Execute( YEAR + i, MONTH + j, FileNameXml);                
            }            
        }

        //message
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nПрограмма выполнена успешно");
        Console.ResetColor();
    }  
    
    private void Execute(int YEAR, int MONTH, string FileNameXml)
    {
        //---MIS---

        //выбираем пакеты из счетов за период
        if (Init.GET_FROM_MISDB == 1 && (Init.TYPE_OUT_XML_RH == 1 || Init.TYPE_OUT_XML_RHE == 1 || Init.GET_EMPTY_FROM_MISDB == 1 || Init.GET_FROM_MISDB_IDENT == 1))
        {
            IEnumerable<Schet>? H_schets = new RepositoryMIS(new MisContext()).GetSchets(YEAR, MONTH).Result;

            if (H_schets != null)
            {
                Task mis = new(() => new App().DoMis(H_schets, FileNameXml));
                mis.Start();
                mis.Wait();
            }
            else if (Init.TYPE_OUT_XML_RH == 1 || Init.TYPE_OUT_XML_RHE == 1)
            {
                Console.WriteLine("\nв заданном периоде не найден(ы) счет(а) {0} из MISDB\n", Init.SCHET_NSCHET_MIS?.Count > 0 ? string.Concat("номер ", Init.SCHET_NSCHET_MIS.ToString()) : "");
            }
        }
           

        //---MTR---

        //выбираем пакеты из счетов за период
        if (Init.GET_FROM_MTRDB == 1 && (Init.TYPE_OUT_XML_RI == 1 || Init.TYPE_OUT_XML_RIE == 1 || Init.GET_EMPTY_FROM_MTRDB == 1))
        {
            IEnumerable<Schet_mtr>? H_schets_mtr = new RepositoryMTR(new MtrContext()).GetSchets(YEAR, MONTH).Result;


            if (H_schets_mtr != null)
            {
                Task mtr = new(() => new App().DoMtr(H_schets_mtr, FileNameXml));
                mtr.Start();
                mtr.Wait();
            }
            else if (Init.TYPE_OUT_XML_RI == 1 || Init.TYPE_OUT_XML_RIE == 1)
            {
                Console.WriteLine("\nв заданном периоде не найден(ы) счет(а) {0} из MTRDB\n", Init.SCHET_NSCHET_MTR?.Count() > 0 ? string.Concat("номер ", Init.SCHET_NSCHET_MTR) : "");
            }
        }                          
    } 

    private void DoMis(IEnumerable<Schet> schets, string FileNameXml)
    {
        int schets_count = schets.Count();
        if (schets_count <= 0)
        {
            // message
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" MIS счета не найдены");
            Console.ResetColor();
            return;
        }

        //массив потоков
        Task[] tasks = new Task[schets.Count()];
        int task_counter = 0;

        // перебираем пакеты
        foreach (var (schet, index) in schets.Select((v, i) => (v, i)))
        {
            if (schet.Id <= 0)
                break;

            //запускаем новый поток
            tasks[task_counter] = new Task(() =>
            {
                Console.WriteLine($"{1 + index} MIS Пакет \"{schet.FILENAME}\" " +
                                    $"из {schets?.Count()} добавлен в поток. ожидайте завершения...");

                CookingMis.Run(schet, string.Concat(FileNameXml, schet.YEAR_REPORT + Initialization.PACKET_MIS_NUM_START, schet.MONTH_REPORT, schet.Id));

                //message
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" MIS Пакет \"{schet.FILENAME}\" готов. осталось {--schets_count}");
                Console.ResetColor();
            });
            tasks[task_counter].Start();

            if (Init.THREAD_ONE)
                tasks[task_counter].Wait();
            task_counter++;
        }

        if(!Init.THREAD_ONE)
            Task.WaitAll(tasks);   
    }

    private void DoMtr(IEnumerable<Schet_mtr> schets, string FileNameXml)
    {
        int schets_count = schets.Count();
        if (schets_count <= 0)
        {
            // message
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" MTR счета не найдены");
            Console.ResetColor();
            return;
        }

        //массив потоков
        Task[] tasks = new Task[schets.Count()];
        int task_counter = 0;

        // перебираем пакеты
        foreach (var (schet, index) in schets.Select((v, i) => (v, i)))
        {
            if (schet.Id <= 0)
                break;

            //запускаем новый поток
            tasks[task_counter] = new Task(() =>
            {
                Console.WriteLine($"{1 + index} MTR Пакет \"{schet.FILENAME}\" " +
                                    $"из {schets?.Count()} добавлен в поток. ожидайте завершения...");

                CookingMtr.Run(schet, string.Concat(FileNameXml, schet.YEAR + Initialization.PACKET_MTR_NUM_START, schet.MONTH, schet.Id));

                //message
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" MTR Пакет \"{schet.FILENAME}\" готов. осталось {--schets_count}");
                Console.ResetColor();
            });
            tasks[task_counter].Start();

            if (Init.THREAD_ONE)
                tasks[task_counter].Wait();

            task_counter++;
        }

        if (!Init.THREAD_ONE)
            Task.WaitAll(tasks);
    }
}