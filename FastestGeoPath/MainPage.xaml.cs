namespace FastestGeoPath;

public partial class MainPage : ContentPage
{
    bool isTimerGoing;
    TimeSpan timerTime;
    readonly IDispatcherTimer timer;

    readonly string textStart = "Press button to start tracking path";
    readonly string textStop = "Press button to stop tracking path";

    public MainPage()
    {
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) =>
        {
            timerTime += TimeSpan.FromSeconds(1);

            UpdateTimerOnScreen();
        };

        InitializeComponent();
    }

    private void OnTrackingClicked(object sender, EventArgs e)
    {
        if (!isTimerGoing)
        {
            timerTime = new TimeSpan();
            timer.Start();
            isTimerGoing = true;

            UpdateTimerOnScreen();

            InstructionLabel.Text = textStop;
            SemanticScreenReader.Announce(InstructionLabel.Text);
        }
        else
        {
            timer.Stop();
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
}

