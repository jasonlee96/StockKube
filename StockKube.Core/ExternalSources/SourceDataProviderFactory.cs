using StockKube.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.ExternalSources
{
    public class SourceDataProviderFactory
    {
        private List<ISourceDataProvider> _providers;
        public SourceDataProviderFactory(IEnumerable<ISourceDataProvider> providers) 
        {
            _providers = providers.ToList();
        }

        public ISourceDataProvider? GetProvider(ExchangeTypeEnum exchangeType)
        {
            return _providers.FirstOrDefault(x => x.ExchangeType == exchangeType) ?? default;
        }
    }
}
