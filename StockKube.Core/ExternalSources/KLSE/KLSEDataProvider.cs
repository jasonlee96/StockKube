using StockKube.Core.Enums;
using StockKube.Core.Models;

namespace StockKube.Core.ExternalSources.KLSE
{
    public class KLSEDataProvider : ISourceDataProvider
    {
        public ExchangeTypeEnum ExchangeType { get => ExchangeTypeEnum.KLSE; }

        public Task<RateDTO> GetRateAsync(SymbolDTO symbol)
        {
            throw new NotImplementedException();
        }
    }
}
