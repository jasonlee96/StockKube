using DAL.Mongo.Models;
using Microsoft.Extensions.Logging;
using StockKube.Core.Cache;
using StockKube.Core.Constants;
using StockKube.Core.Enums;
using StockKube.Core.Extensions;
using StockKube.Core.ExternalSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace StockKube.Core.Services
{
    public class RateRetrieverBackgroundService : TimerBackgroundService
    {
        private readonly ILogger _logger;
        private readonly ISourceDataProvider _provider;
        private readonly ICacheAppSettingService _cacheAppSettingService;
        public RateRetrieverBackgroundService(ILogger logger, ISourceDataProvider provider, int interval, ICacheAppSettingService appSettingService) : base(logger, interval * 60 * 1000)
        {
            _logger = logger;
            _provider = provider;
            _cacheAppSettingService = appSettingService;
        }
        protected override void TimerEventStarted(object? sender, ElapsedEventArgs e)
        {
            try
            {
                _logger.Log($"Getting Rate... {_provider.ExchangeType.ToString()}");

                var symbols = _cacheAppSettingService.GetSetting<List<Watchlist>>(string.Format(CoreConstants.KEY_FORMAT_ITEMS, CoreConstants.WATCHLIST, _provider.ExchangeType.ToString()));
                
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
