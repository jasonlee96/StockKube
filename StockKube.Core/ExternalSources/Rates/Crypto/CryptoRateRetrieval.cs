using StockKube.Core.Models;

namespace StockKube.Core.ExternalSources.Rates.Crypto
{
    public class CryptoRateRetrieval : IRateRetrieval
    {
        public Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            throw new NotImplementedException();
        }
    }
}
