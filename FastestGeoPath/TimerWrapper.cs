namespace FastestGeoPath
{
    /// <summary>
    /// Wrapper for IDispatcherTimer.
    /// </summary>
    public class TimerWrapper
    {
        /// <summary>
        /// Indication that the timer is running.
        /// </summary>
        public bool IsGoing => Timer.IsRunning;

        /// <summary>
        /// Time of timer.
        /// </summary>
        public TimeSpan Time { get; private set; } = new TimeSpan();

        /// <summary>
        /// An instance of the system timer.
        /// </summary>
        private IDispatcherTimer Timer { get; set; }

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
            Timer.Tick += (s, e) => Time += StandardInterval;
        }

        /// <summary>
        /// Add event handler, that will called on every timer operation.
        /// </summary>
        /// <param name="eventHandler"></param>
        public void AddEventHandler(EventHandler eventHandler) => Timer.Tick += eventHandler;

        /// <summary>
        /// Start.
        /// </summary>
        public void Start() => Timer.Start();

        /// <summary>
        /// Stop.
        /// </summary>
        public void Stop() => Timer.Stop();
    }
}
