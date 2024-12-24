using StockKube.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.ExternalSources.Rates
{
    public interface IRateRetrieval
    {
        public async Task<RateDTO> GetRateAsync(string symbol);
    }
}
