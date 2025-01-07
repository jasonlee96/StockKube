using DAL.Mongo.Models;
using DAL.Mongo.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using StockKube.Core.Constants;
using StockKube.Core.Extensions;
using StockKube.Core.Models;

namespace StockKube.Core.Cache
{
    public class CacheAppSettingService : ICacheAppSettingService
    {
        private readonly ILogger _logger;
        private readonly IExternalSourceRepository _externalSourceRepository;

        private List<CacheKeyDTO> _keys;
        public CacheAppSettingService(ILogger<CacheAppSettingService> logger, IExternalSourceRepository externalSourceRepo) 
        { 
            _logger = logger;
            _externalSourceRepository = externalSourceRepo;
            _keys = new List<CacheKeyDTO>();
            PreloadOrReloadAllSettingAsync().GetAwaiter().GetResult();
        }

        public async Task<T> GetSettingAsync<T>(string key)
        {
            if(string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            var matchedKey = _keys.Where(x => x.Key.ToUpper() == key.ToUpper()).FirstOrDefault();

            if (matchedKey != null)
            {
                return await Task.FromResult(matchedKey.Value.DeserializeToPoco<T>());
            }
            return default;
        }

        public async Task PreloadOrReloadAllSettingAsync()
        {
            // r&d a pipeline to  fetch all the keys
            List<Task> tasks = new List<Task>();
            tasks.Add(FetchExternalSourcesAsync());

            await Task.WhenAll(tasks);
        }

        private async Task FetchExternalSourcesAsync()
        {
            try
            {
                var extSources = await _externalSourceRepository.GetAllExternalSourcesAsync();

                _keys.Add(new CacheKeyDTO {
                    DataType = Enums.DataTypeEnum.Object,
                    Key = string.Format(CoreConstants.KEY_FORMAT, CoreConstants.EXTERNAL_SOURCE),
                    Value = extSources.ToJson(),
                    ClassName = typeof(List<ExternalSource>)?.FullName ?? ""
                });
            }
            catch (Exception ex) 
            {
                _logger.Log(ex);
            }
        }
    }
}
