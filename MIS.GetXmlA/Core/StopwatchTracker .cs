using System.Diagnostics;

public class StopwatchTracker : IDisposable
{
    public readonly Stopwatch _stopwatch;
    private readonly Action<long> _onDispose;

    private bool _disposed = false;

    private StopwatchTracker(Stopwatch stopwatch, Action<long> onDispose)
    {
        _stopwatch = stopwatch;
        _onDispose = onDispose;
    }

    public static StopwatchTracker StartNew(Action<long> onElapsedMilliseconds)
    {
        var sw = Stopwatch.StartNew();
        return new StopwatchTracker(sw, onElapsedMilliseconds);
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _stopwatch.Stop();
            _onDispose?.Invoke(_stopwatch.ElapsedMilliseconds);
            _disposed = true;
        }
    }
}
