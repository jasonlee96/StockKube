using DAL.Mongo.Repositories.Interfaces;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IStockRateRepository _stockRateRepository;

        public Worker(ILogger<Worker> logger, IStockRateRepository stockRateRepository)
        {
            _logger = logger;
            _stockRateRepository = stockRateRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var x = _stockRateRepository.GetRateBySymbol("GREATECH");
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
    // 1. polygon API / https://eodhd.com/financial-apis/live-realtime-stocks-api  / https://eodhd.com/exchange/KLSE
    // 1. Worker 1 -> capture rate from API and record into it (rate, volumne, etc) -- prioritize this first.
    // 1. Worker 2 -> capture last n day data and compute avg, peak. 
    // 1. worker 3 -> cpature daily performance (open, close)
    // 1 UI to add / remove symbols.
}
