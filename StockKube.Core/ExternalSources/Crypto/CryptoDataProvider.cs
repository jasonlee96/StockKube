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
        public ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.CRYPTO;

        public CryptoDataProvider(IHTTPClientService httpService) 
        {
            _httpService = httpService;
            // retrieve api info from mongo
            _binder = new APIRequestBinder
            {
                Headers = new Dictionary<string, string>
                {
                    { "authorization", "api-key" }
                },
                Url = ""
            };
            

        }
        public Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            //_httpService.PostAsync()
            throw new NotImplementedException();
        }
    }

}
