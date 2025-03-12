using DAL.Mongo.Models;
using DAL.Mongo.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using StockKube.Core.Constants;
using StockKube.Core.Enums;
using StockKube.Core.Extensions;
using StockKube.Core.Models;

namespace StockKube.Core.Cache
{
    public class CacheAppSettingService : ICacheAppSettingService
    {
        private readonly ILogger _logger;
        private readonly IExternalSourceRepository _externalSourceRepository;
        private readonly IWatchlistRepository _watchlistRepository;

        private List<CacheKeyDTO> _keys;
        public CacheAppSettingService(ILogger<CacheAppSettingService> logger, IExternalSourceRepository externalSourceRepo, IWatchlistRepository watchlistRepository) 
        { 
            _logger = logger;
            _externalSourceRepository = externalSourceRepo;
            _keys = new List<CacheKeyDTO>();
            _watchlistRepository = watchlistRepository;
            PreloadOrReloadAllSettingAsync().GetAwaiter().GetResult();
        }

        public T GetSetting<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            var matchedKey = _keys.Where(x => x.Key.ToUpper() == key.ToUpper()).FirstOrDefault();

            if (matchedKey != null)
            {
                return matchedKey.Value.DeserializeToPoco<T>();
            }
            return default;
        }

        public async Task<T> GetSettingAsync<T>(string key)
        {
            return await Task.FromResult(GetSetting<T>(key));
        }

        public async Task PreloadOrReloadAllSettingAsync()
        {
            // r&d a pipeline to  fetch all the keys
            List<Task> tasks = new List<Task>();
            tasks.Add(FetchExternalSourcesAsync());
            tasks.Add(GetWatchlistAsync());

            await Task.WhenAll(tasks);
        }

        private async Task FetchExternalSourcesAsync()
        {
            try
            {
                var extSources = await _externalSourceRepository.GetAllExternalSourcesAsync();

                _keys.Add(new CacheKeyDTO
                {
                    DataType = Enums.DataTypeEnum.Object,
                    Key = string.Format(CoreConstants.KEY_FORMAT, CoreConstants.EXTERNAL_SOURCE),
                    Value = extSources.ToJson(),
                    ClassName = typeof(List<ExternalSource>)?.FullName ?? ""
                });
                // form item as well
                foreach (var extSource in extSources)
                {
                    _keys.Add(new CacheKeyDTO
                    {
                        DataType = Enums.DataTypeEnum.Object,
                        Key = string.Format(CoreConstants.KEY_FORMAT_ITEMS, CoreConstants.EXTERNAL_SOURCE, extSource.ExchangeType),
                        Value = extSource.ToJson(),
                        ClassName = typeof(ExternalSource)?.FullName ?? ""
                    });
                }
            }
            catch (Exception ex) 
            {
                _logger.Log(ex);
            }
        }

        private async Task GetWatchlistAsync()
        {
            try
            {
                var currentWatchlist = await _watchlistRepository.GetAllWatchlistAsync();

                currentWatchlist.GroupBy(x => x.ExchangeType, (k, v) => new { Key = k, Items = v }).ToList().ForEach(x => {
                    _keys.Add(new CacheKeyDTO
                    {
                        DataType = Enums.DataTypeEnum.Object,
                        Key = string.Format(CoreConstants.KEY_FORMAT_ITEMS, CoreConstants.WATCHLIST, x.Key),
                        Value = x.Items.ToJson(),
                        ClassName = typeof(List<Watchlist>)?.FullName ?? ""
                    });
                });
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }
        }
    }
}
