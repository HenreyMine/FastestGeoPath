namespace FastestGeoPath.Exceptions;

/// <summary>
/// Exception when timer was not add to dispatcher.
/// </summary>
public class NoDispatcherForTimerException : Exception
{
  /// <summary>
  /// Constructor.
  /// </summary>
  public NoDispatcherForTimerException() : base("Timer was not add to dispatcher.") { }
}
