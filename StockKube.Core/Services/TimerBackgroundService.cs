
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StockKube.Core.Services
{
    public abstract class TimerBackgroundService : BackgroundService
    {
        // timer obj
        private System.Timers.Timer _timer;
        private readonly ILogger _logger;
        private double _interval;
        public DateTime? startTime;
        public TimerBackgroundService(ILogger logger,double intervalInMillisecond)
        {
            _interval = intervalInMillisecond;
            _timer = new System.Timers.Timer();
            _logger = logger;
        }

        protected abstract void TimerEventStarted(object? sender, System.Timers.ElapsedEventArgs e);
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double tickTime = _interval;
            if(startTime != null)
            {
                var currentPeriod = DateTime.Now;
                while (startTime.Value < currentPeriod)
                {
                    startTime = startTime.Value.AddMilliseconds(_interval);
                }
                tickTime = (double)(startTime.Value - currentPeriod).TotalMilliseconds;
            }
            _timer.Interval = tickTime;
            _timer.Elapsed += (s, e) => { _timer.Interval = _interval; }; // set interval to 24 hours
            _timer.Elapsed += TimerEventStarted;
            _timer.Start();
            _logger.LogInformation($"[TimerIntialization]: initialized interval:{_interval}ms");
            return Task.CompletedTask;
        }
    }
}
