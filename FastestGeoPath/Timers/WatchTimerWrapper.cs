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

  /// <summary>
  /// Standard time to add when timer triggered.
  /// </summary>
  private readonly TimeSpan StandardTimeToAdd = TimeSpan.FromSeconds(1);

  /// <summary>
  /// Add timer for watch that add time when timer triggered.
  /// </summary>
  /// <param name="dispatcher">Dispatcher where timer added.</param>
  /// <param name="interval">Interval for timer. Default: 1 second.</param>
  /// <param name="timeToAdd">Time that will be added when timer triggered. Default: 1 second.</param>
  public void AddToDispatcher(IDispatcher dispatcher, TimeSpan? interval = null, TimeSpan? timeToAdd = null)
  {
    base.AddToDispatcher(dispatcher, interval);
    var notNullTimeToAdd = timeToAdd ??= StandardTimeToAdd;
    Timer.Tick += (s, e) => Time += notNullTimeToAdd;
  }

  /// <summary>
  /// Stop and reset time of timer.
  /// </summary>
  public void Reset()
  {
    Stop();
    Time = TimeSpan.Zero;
  }
}
