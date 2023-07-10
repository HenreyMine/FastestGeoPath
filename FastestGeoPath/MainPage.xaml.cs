namespace FastestGeoPath;

public partial class MainPage : ContentPage
{
    //bool isTimerGoing;
    //TimeSpan timerTime;
    //readonly IDispatcherTimer timer;

    readonly string textStart = "Press button to start tracking path";
    readonly string textStop = "Press button to stop tracking path";

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    readonly IDispatcherTimer locationTimer;

    public MainPage()
    {
        //timer = Dispatcher.CreateTimer();
        //timer.Interval = TimeSpan.FromSeconds(1);
        //timer.Tick += (s, e) =>
        //{
            //timerTime += TimeSpan.FromSeconds(1);

            //UpdateTimerOnScreen();
        //};

        locationTimer = Dispatcher.CreateTimer();
        locationTimer.Interval = TimeSpan.FromSeconds(10);
        locationTimer.Tick += async (s, e) => await GetCurrentLocation();

        InitializeComponent();
    }

    private async void OnTrackingClicked(object sender, EventArgs e)
    {
        if (!isTimerGoing)
        {
            //timerTime = new TimeSpan();
            timer.Start();
            //isTimerGoing = true;

            //UpdateTimerOnScreen();

            locationTimer.Start();
            await GetCurrentLocation();

            InstructionLabel.Text = textStop;
            SemanticScreenReader.Announce(InstructionLabel.Text);
        }
        else
        {
            timer.Stop();
            locationTimer.Stop();
            isTimerGoing = false;
            InstructionLabel.Text = textStart;
            SemanticScreenReader.Announce(InstructionLabel.Text);
        }
    }

    private void UpdateTimerOnScreen()
    {
        TimerLabel.Text = timerTime.ToString();
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

