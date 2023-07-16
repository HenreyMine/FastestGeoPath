namespace FastestGeoPath;

public partial class MainPage : ContentPage
{
    readonly TimerWrapper pathTimer = new();
    readonly TimerWrapper geoTimer = new();

    readonly string textStart = "Press button to start tracking path";
    readonly string textStop = "Press button to stop tracking path";

    public MainPage()
    {
        pathTimer.AddToDispatcher(Dispatcher);
        pathTimer.AddEventHandler(UpdateTimerOnScreenEventHandler);

        geoTimer.AddToDispatcher(Dispatcher, TimeSpan.FromSeconds(10));
        //geoTimer.AddEventHandler();

        InitializeComponent();
    }

    private async void OnTrackingClicked(object sender, EventArgs e)
    {
        if (!pathTimer.IsGoing)
        {
            pathTimer.Start();
            UpdateTimerOnScreenEventHandler();

            geoTimer.Start();

            UpdateInstructionLabelText(textStop);
        }
        else
        {
            pathTimer.Stop();
            geoTimer.Stop();
            UpdateInstructionLabelText(textStart);
        }
    }

    private void UpdateTimerOnScreenEventHandler(object eventObject = null, EventArgs eventArgs = null)
    {
        TimerLabel.Text = pathTimer.Time.ToString();
        SemanticScreenReader.Announce(TimerLabel.Text);
    }

    private void UpdateInstructionLabelText(string text)
    {
        InstructionLabel.Text = text;
        SemanticScreenReader.Announce(InstructionLabel.Text);
    }


    public async Task GetCurrentLocation()
    {
        try
        {
            GeolocationRequest request = new(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));

            Location location = await Geolocation.Default.GetLocationAsync(request, new CancellationToken());

            if (location != null)
            {
                LocationLabel.Text = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                SemanticScreenReader.Announce(LocationLabel.Text);
            }
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }
    }
}

