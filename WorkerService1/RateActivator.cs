using DAL.Mongo.Models;
using StockKube.Core.Cache;
using StockKube.Core.Constants;
using StockKube.Core.Enums;
using StockKube.Core.Extensions;
using StockKube.Core.ExternalSources;
using StockKube.Core.Helpers;
using StockKube.Core.Services;

namespace WorkerService1
{
    public class RateActivator : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ICacheAppSettingService _appSettingService;
        private List<RateRetrieverBackgroundService> _backgroundServices;
        private readonly SourceDataProviderFactory _factory;

        public RateActivator(ILogger<RateActivator> logger, ICacheAppSettingService appSettingService, SourceDataProviderFactory factory)
        {
            _logger = logger;
            _appSettingService = appSettingService;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            // 1. get all sources
            var sources = await _appSettingService.GetSettingAsync<List<ExternalSource>>(string.Format(CoreConstants.KEY_FORMAT, CoreConstants.EXTERNAL_SOURCE));

            _backgroundServices = new List<RateRetrieverBackgroundService>();
            foreach (var source in sources)
            {
                var provider = _factory.GetProvider(source.ExchangeType.ToEnum<ExchangeTypeEnum>());
                _backgroundServices.Add(new RateRetrieverBackgroundService(_logger, provider, source.IntervalInMin, _appSettingService));
            }

            if (_backgroundServices.Any())
            {
                await Task.WhenAll(_backgroundServices.Select(x => x.StartAsync(stoppingToken)));
                _logger.Log("Rate Background services activated");
            }
            
        }
    }
    // 1. polygon API / https://eodhd.com/financial-apis/live-realtime-stocks-api  / https://eodhd.com/exchange/KLSE
    // 1. Worker 1 -> capture rate from API and record into it (rate, volumne, etc) -- prioritize this first.
    // 1. Worker 2 -> capture last n day data and compute avg, peak. 
    // 1. worker 3 -> cpature daily performance (open, close)
    // 1 UI to add / remove symbols.
}
