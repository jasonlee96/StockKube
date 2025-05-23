using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.ExternalSources.Crypto
{
    public static class CryptoDaraProviderConstanst
    {
        public static string GET_RATE_URL = "/v2/aggs/ticker/X:{0}/range/1/hour/{1}/{2}";
    }
}
