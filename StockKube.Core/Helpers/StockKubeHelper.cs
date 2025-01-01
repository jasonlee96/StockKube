using DAL.Mongo.Models;
using StockKube.Core.Models.IntraObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Helpers
{
    public static class StockKubeHelper
    {
        public static APIRequestBinder FormApiHTTPBinding(ExternalSource source)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", source.APIKey);

            return new APIRequestBinder
            {
                Url = source.APIUrl,
                Headers = headers
            };
        }
    }
}
