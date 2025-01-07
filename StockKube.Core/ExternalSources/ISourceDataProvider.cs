using StockKube.Core.Enums;
using StockKube.Core.Models;

namespace StockKube.Core.ExternalSources
{
    public interface ISourceDataProvider
    {
        ExchangeTypeEnum ExchangeType { get; }
        Task<RateDTO> GetRateAsync(SymbolDTO symbol);
    }
}
