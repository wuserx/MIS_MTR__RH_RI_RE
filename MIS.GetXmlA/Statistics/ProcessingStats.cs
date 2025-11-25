public class ProcessingStats
{
    private long _misSubmitted;
    private long _misProcessed;
    private long _misErrors;

    private long _mtrSubmitted;
    private long _mtrProcessed;
    private long _mtrErrors;

    // Открытые свойства — только для чтения
    public long MisSubmitted => Volatile.Read(ref _misSubmitted);
    public long MisProcessed => Volatile.Read(ref _misProcessed);
    public long MisErrors => Volatile.Read(ref _misErrors);

    public long MtrSubmitted => Volatile.Read(ref _mtrSubmitted);
    public long MtrProcessed => Volatile.Read(ref _mtrProcessed);
    public long MtrErrors => Volatile.Read(ref _mtrErrors);

    // Методы инкремента — потокобезопасные
    public void SubmitMis() => Interlocked.Increment(ref _misSubmitted);
    public void SuccessMis() => Interlocked.Increment(ref _misProcessed);
    public void ErrorMis() => Interlocked.Increment(ref _misErrors);

    public void SubmitMtr() => Interlocked.Increment(ref _mtrSubmitted);
    public void SuccessMtr() => Interlocked.Increment(ref _mtrProcessed);
    public void ErrorMtr() => Interlocked.Increment(ref _mtrErrors);

    public void Print()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("ИТОГОВАЯ СТАТИСТИКА ОБРАБОТКИ:");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"MIS: отправлено — {MisSubmitted}, обработано — {MisProcessed}, ошибок — {MisErrors}");
        Console.WriteLine($"MTR: отправлено — {MtrSubmitted}, обработано — {MtrProcessed}, ошибок — {MtrErrors}");
        Console.WriteLine($"Общее: {MisProcessed + MtrProcessed} пакетов успешно обработано.");
        Console.ResetColor();
    }
}
