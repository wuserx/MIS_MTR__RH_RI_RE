public class ProcessingStats
{
    private long _misSubmitted;
    private long _misProcessed;
    private long _misErrors;

    private long _mtrSubmitted;
    private long _mtrProcessed;
    private long _mtrErrors;

    private long _totalProcessingTimeMs;  // Общее время обработки в миллисекундах

    private long _totalExpected;  // Общее количество пакетов, которые должны быть обработаны
    public void SetTotalExpected(long total) => _totalExpected = total;

    public long TotalProcessed => MisProcessed + MtrProcessed;
    public long TotalErrors => MisErrors + MtrErrors;

    public int Percentage => _totalExpected == 0
        ? 0
        : (int)((TotalProcessed + TotalErrors) * 100 / _totalExpected);

    public long MisSubmitted => Volatile.Read(ref _misSubmitted);
    public long MisProcessed => Volatile.Read(ref _misProcessed);
    public long MisErrors => Volatile.Read(ref _misErrors);

    public long MtrSubmitted => Volatile.Read(ref _mtrSubmitted);
    public long MtrProcessed => Volatile.Read(ref _mtrProcessed);
    public long MtrErrors => Volatile.Read(ref _mtrErrors);

    public double AverageProcessingTimeMs => (MisProcessed + MtrProcessed) == 0
        ? 0
        : _totalProcessingTimeMs / (double)(MisProcessed + MtrProcessed);

    public string AverageProcessingTimeFormatted => TimeSpan.FromMilliseconds(AverageProcessingTimeMs).ToString(@"ss\.ff") + " сек";

    public void SubmitMis() => Interlocked.Increment(ref _misSubmitted);
    public void SuccessMis()
    {
        Interlocked.Increment(ref _misProcessed);
        UpdateProgress(); // ✅ Обновляем
    }

    public void ErrorMis()
    {
        Interlocked.Increment(ref _misErrors);
        UpdateProgress(); // ✅ Обновляем
    }

    public void SubmitMtr() => Interlocked.Increment(ref _mtrSubmitted);
    public void SuccessMtr()
    {
        Interlocked.Increment(ref _mtrProcessed);
        UpdateProgress(); // ✅ Обновляем
    }

    public void ErrorMtr()
    {
        Interlocked.Increment(ref _mtrErrors);
        UpdateProgress(); // ✅ Обновляем
    }

    public void AddProcessingTime(long elapsedMilliseconds)
        => Interlocked.Add(ref _totalProcessingTimeMs, elapsedMilliseconds);

    public void UpdateProgress()
    {
        // Если вывод перенаправлен (например, в файл) — не показываем
        if (Console.IsOutputRedirected) return;

        try
        {
            // Фиксируем строку прогресс-бара — всегда строка 0
            int barTop = 0;

            // Сохраняем текущую позицию, чтобы вернуться
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            // Обновляем прогресс-бар на первой строке
            Console.SetCursorPosition(0, barTop);

            int width = Console.WindowWidth - 1; // Защита от исключений

            string progressBar = new string('█', Percentage / 2) + new string('░', 50 - Percentage / 2);
            if (progressBar.Length > 50) progressBar = progressBar.Substring(0, 50);

            string message = $"📌 Прогресс: [{progressBar}] {TotalProcessed + TotalErrors}/{_totalExpected} ({Percentage}%) | " +
                             $"MIS: {MisProcessed + MisErrors} | MTR: {MtrProcessed + MtrErrors} | Ошибки: {TotalErrors}";

            // Обрезаем под ширину
            if (message.Length > width) message = message.Substring(0, width);

            Console.Write(message.PadRight(width));

            // Возвращаем курсор на предыдущее место (для логов)
            if (currentTop >= 1) // не возвращаемся на строку прогресса
                Console.SetCursorPosition(
                    Math.Min(currentLeft, Console.WindowWidth - 1),
                    Math.Min(currentTop, Console.WindowHeight - 1)
                );
        }
        catch (IOException)
        {
            // Консоль может быть временно недоступна
        }
        catch (ArgumentOutOfRangeException)
        {
            // Размеры консоли изменились — игнорируем
        }
    }


    public void Print()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📊 ИТОГОВАЯ СТАТИСТИКА ОБРАБОТКИ:");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"MIS: отправлено — {MisSubmitted}, обработано — {MisProcessed}, ошибок — {MisErrors}");
        Console.WriteLine($"MTR: отправлено — {MtrSubmitted}, обработано — {MtrProcessed}, ошибок — {MtrErrors}");
        Console.WriteLine($"Общее: {MisProcessed + MtrProcessed} пакетов успешно обработано.");
        Console.WriteLine($"⏱️  Среднее время обработки: {AverageProcessingTimeFormatted}");
        Console.ResetColor();
    }
}
