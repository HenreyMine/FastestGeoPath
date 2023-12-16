using FastestGeoPath.Exceptions;

namespace FastestGeoPath.Timers;

/// <summary>
/// Wrapper for IDispatcherTimer.
/// </summary>
public class TimerWrapper
{
  /// <summary>
  /// Indication that the timer is running.
  /// </summary>
  public bool IsGoing => Timer?.IsRunning ?? throw new NoDispatcherForTimerException();

  /// <summary>
  /// An instance of the system timer.
  /// </summary>
  protected IDispatcherTimer? Timer { get; set; }

  /// <summary>
  /// Standard interval.
  /// </summary>
  private readonly TimeSpan StandardInterval = TimeSpan.FromSeconds(1);

  /// <summary>
  /// Adds a timer to the dispatcher.
  /// </summary>
  /// <param name="dispatcher">Dispatcher where timer added.</param>
  /// <param name="interval">Interval for timer. Default: 1 second.</param>
  public void AddToDispatcher(IDispatcher dispatcher, TimeSpan? interval = null)
  {
    Timer = dispatcher.CreateTimer();
    Timer.Interval = interval ?? StandardInterval;
  }

  /// <summary>
  /// Add event handler, that will called on every timer operation.
  /// </summary>
  /// <param name="eventHandler"></param>
  public void AddEventHandler(EventHandler eventHandler)
  {
    if (Timer is null)
      throw new NoDispatcherForTimerException();
    Timer.Tick += eventHandler;
  }

  /// <summary>
  /// Start timer.
  /// </summary>
  public void Start()
  {
    if (Timer is null)
      throw new NoDispatcherForTimerException(); 
    Timer.Start();
  }

  /// <summary>
  /// Stop timer.
  /// </summary>
  public void Stop()
  {
    if (Timer is null)
      throw new NoDispatcherForTimerException();
    Timer.Stop();
  }
}
