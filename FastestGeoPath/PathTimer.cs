namespace FastestGeoPath
{
    /// <summary>
    /// Path timer.
    /// </summary>
    public class PathTimer
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
        private readonly TimeSpan standardInterval = TimeSpan.FromSeconds(1);

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatcher">Dispatcher where timer added.</param>
        public void AddTimer(IDispatcher dispatcher)
        {
            Timer = dispatcher.CreateTimer();
            Timer.Interval = standardInterval;
            Timer.Tick += (s, e) => Time += standardInterval;
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
