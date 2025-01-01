using StockKube.Core.Models;

namespace StockKube.Core.ExternalSources.Rates.Crypto
{
    public class CryptoDataProvider : ISourceDataProvider
    {
        public Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            throw new NotImplementedException();
        }
    }
}
