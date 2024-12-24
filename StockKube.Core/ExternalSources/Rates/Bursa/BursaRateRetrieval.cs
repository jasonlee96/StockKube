using StockKube.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.ExternalSources.Rates.Bursa
{
    public class BursaRateRetrieval : IRateRetrieval
    {
        public Task<RateDTO> GetRateAsync(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}
