using StockKube.Core.Enums;
using StockKube.Core.Models;

namespace StockKube.Core.ExternalSources.Crypto
{
    public class CryptoDataProvider : ISourceDataProvider
    {
        public ExchangeTypeEnum ExchangeType => ExchangeTypeEnum.CRYPTO;

        public Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            throw new NotImplementedException();
        }
    }
}
