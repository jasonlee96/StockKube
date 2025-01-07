using Microsoft.Extensions.Logging;
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
        public RateRetrieverBackgroundService(ILogger logger, ISourceDataProvider provider, int interval) : base(logger, interval)
        {
            _logger = logger;
            _provider = provider;
        }
        protected override void TimerEventStarted(object? sender, ElapsedEventArgs e)
        {
            try
            {
                _logger.Log($"Getting Rate... {_provider.ExchangeType.ToString()}");
                //_provider.GetRateAsync()
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
