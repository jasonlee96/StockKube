using StockKube.Core.Models;

namespace StockKube.Core.ExternalSources.Crypto
{
    public class CryptoDataProvider : ISourceDataProvider
    {
        public Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            throw new NotImplementedException();
        }
    }
}
