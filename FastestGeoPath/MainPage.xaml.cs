namespace FastestGeoPath;

public partial class MainPage : ContentPage
{
    readonly TimerWrapper pathTimer = new();

    readonly string textStart = "Press button to start tracking path";
    readonly string textStop = "Press button to stop tracking path";

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    readonly IDispatcherTimer locationTimer;

    public MainPage()
    {
        pathTimer.AddToDispatcher(Dispatcher);
        pathTimer.AddEventHandler(UpdateTimerOnScreenEventHandler);

        locationTimer = Dispatcher.CreateTimer();
        locationTimer.Interval = TimeSpan.FromSeconds(10);
        locationTimer.Tick += async (s, e) => await GetCurrentLocation();

        InitializeComponent();
    }

    private async void OnTrackingClicked(object sender, EventArgs e)
    {
        if (!pathTimer.IsGoing)
        {
            pathTimer.Start();
            UpdateTimerOnScreenEventHandler();

            locationTimer.Start();
            await GetCurrentLocation();

            InstructionLabel.Text = textStop;
            SemanticScreenReader.Announce(InstructionLabel.Text);
        }
        else
        {
            pathTimer.Stop();
            locationTimer.Stop();
            InstructionLabel.Text = textStart;
            SemanticScreenReader.Announce(InstructionLabel.Text);
        }
    }

    private void UpdateTimerOnScreenEventHandler(object eventObject = null, EventArgs eventArgs = null)
    {
        TimerLabel.Text = pathTimer.Time.ToString();
        SemanticScreenReader.Announce(TimerLabel.Text);
    }

    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

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
        finally
        {
            _isCheckingLocation = false;
        }
    }

    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}

