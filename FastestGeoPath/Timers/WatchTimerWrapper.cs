namespace FastestGeoPath.Timers;

/// <summary>
/// Wrapper for watch timer.
/// </summary>
public class WatchTimerWrapper : TimerWrapper
{
    /// <summary>
    /// Time of timer.
    /// </summary>
    public TimeSpan Time { get; private set; } = new TimeSpan();

    public void AddToDispatcher(IDispatcher dispatcher, TimeSpan? interval = null)
    {
        base.AddToDispatcher(dispatcher, interval);
        Timer.Tick += (s, e) => Time += TimeSpan.FromSeconds(1);
    }
}
