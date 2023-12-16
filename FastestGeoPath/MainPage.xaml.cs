using FastestGeoPath.Timers;

namespace FastestGeoPath;

/// <summary>
/// Main page of application.
/// </summary>
public partial class MainPage : ContentPage
{
  /// <summary>
  /// Timer for path time.
  /// </summary>
  readonly WatchTimerWrapper pathTimer = new();

  /// <summary>
  /// Timer for getting geo in path. 
  /// </summary>
  readonly TimerWrapper geoTimer = new();

  /// <summary>
  /// Constructor.
  /// </summary>
  public MainPage()
  {
    pathTimer.AddToDispatcher(Dispatcher);
    pathTimer.AddEventHandler(UpdateTimerOnScreenEventHandler);

    geoTimer.AddToDispatcher(Dispatcher, TimeSpan.FromSeconds(10));
    geoTimer.AddEventHandler(UpdateLocationOnScreenEventHandler);

    InitializeComponent();
  }

  /// <summary>
  /// Event that update time of path.
  /// </summary>
  /// <param name="eventObject">Event object.</param>
  /// <param name="eventArgs">Event arguments.</param>
  private void UpdateTimerOnScreenEventHandler(object eventObject = null, EventArgs eventArgs = null)
  {
    TimerLabel.Text = pathTimer.Time.ToString();
    SemanticScreenReader.Announce(TimerLabel.Text);
  }

  /// <summary>
  /// Event after clicked on tracking button.
  /// </summary>
  /// <param name="sender">Sender of event.</param>
  /// <param name="e">Event arguments.</param>
  private void OnTrackingClicked(object sender, EventArgs e)
  {
    if (!pathTimer.IsGoing)
    {
      pathTimer.Start();
      UpdateTimerOnScreenEventHandler();

      geoTimer.Start();

      UpdateInstructionLabelText("Press button to stop tracking path");
    }
    else
    {
      pathTimer.Reset();
      geoTimer.Stop();
      UpdateInstructionLabelText("Press button to start tracking path");
    }
  }

  /// <summary>
  /// Update text of instruction for tracking button.
  /// </summary>
  /// <param name="text">Instruction text.</param>
  private void UpdateInstructionLabelText(string text)
  {
    InstructionLabel.Text = text;
    SemanticScreenReader.Announce(InstructionLabel.Text);
  }

  /// <summary>
  /// Event that update location on screen.
  /// </summary>
  /// <param name="eventObject">Object of event.</param>
  /// <param name="eventArgs">Event arguments.</param>
  public async void UpdateLocationOnScreenEventHandler(object eventObject = null, EventArgs eventArgs = null)
  {
    try
    {
      var location = await GeoService.GetLocation(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
      if (location != null)
        LocationLabel.Text =
            $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
    }
    catch (FeatureNotSupportedException)
    {
      LocationLabel.Text = "Sorry, but getting location not supported on this platform";
    }
    catch (FeatureNotEnabledException)
    {
      LocationLabel.Text = "Geo location not enabled. Please, turn it on";
    }
    catch (PermissionException)
    {
      LocationLabel.Text = "Can't get geo location. Give permission for this app";
    }

    SemanticScreenReader.Announce(LocationLabel.Text);
  }
}

