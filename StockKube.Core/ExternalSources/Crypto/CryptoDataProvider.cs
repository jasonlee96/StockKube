using DAL.Mongo.Models;
using DAL.Mongo.Repositories.Interfaces;
using StockKube.Core.Cache;
using StockKube.Core.Constants;
using StockKube.Core.Enums;
using StockKube.Core.Models;
using StockKube.Core.Models.IntraObjects;
using StockKube.Core.Services.Http;

namespace StockKube.Core.ExternalSources.Crypto
{
    public class CryptoDataProvider : ISourceDataProvider
    {
        private APIRequestBinder _binder;
        private IHTTPClientService _httpService;
        private ICacheAppSettingService _appSetting;
        public ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.CRYPTO;

        public CryptoDataProvider(IHTTPClientService httpService, ICacheAppSettingService setting) 
        {
            _httpService = httpService;
            _appSetting = setting;
            var source = _appSetting.GetSetting<ExternalSource>(string.Format(CoreConstants.KEY_FORMAT_ITEMS, CoreConstants.EXTERNAL_SOURCE, ExchangeType.ToString()));
            // retrieve api info from mongo
            _binder = new APIRequestBinder
            {
                Headers = new Dictionary<string, string>
                {
                    { "authorization", "Bearer " + source.APIKey }
                },
                Url = source.APIUrl,
            };
            

        }
        public async Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            var currentTimestamp = DateTime.UtcNow.AddDays(-1);
            var url = string.Format(CryptoDaraProviderConstanst.GET_RATE_URL, symbol.Symbol, currentTimestamp.AddDays(-1).Ticks, currentTimestamp.Ticks);
            var queryParam = new Dictionary<string, string>
            {
                { "adjusted", "true"},
                { "sort", "asc" },
            };
            var result = await _httpService.GetAsync<GetRateResponse>(url, queryParam, _binder);
            return new RateDTO();
        }
    }

}
