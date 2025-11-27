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
        if (!Console.IsOutputRedirected)
        {
            // Сохраняем текущую строку, чтобы перезаписать её
            var currentTop = Console.CursorTop;
            var currentLeft = Console.CursorLeft;

            // Перемещаемся в начало строки (или резервируем строку ниже)
            Console.SetCursorPosition(0, Console.CursorTop);

            string progressBar = new string('█', Percentage / 2) + new string('░', 50 - Percentage / 2);

            string message = $"Обработка: [{progressBar}] {TotalProcessed + TotalErrors}/{_totalExpected} ({Percentage}%) | " +
                             $"MIS: {MisProcessed + MisErrors} | " +
                             $"MTR: {MtrProcessed + MtrErrors} | " +
                             $"Ошибки: {TotalErrors}";

            // Обрезаем, если длиннее ширины консоли
            if (message.Length >= Console.WindowWidth)
                message = message.Substring(0, Console.WindowWidth - 1);

            Console.Write(message.PadRight(Console.WindowWidth - 1)); // Очищаем остаток строки

            // Возвращаем курсор на следующую строку
            Console.SetCursorPosition(currentLeft, currentTop + 1);
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
