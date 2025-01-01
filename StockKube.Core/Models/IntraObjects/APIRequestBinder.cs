using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Models.IntraObjects
{
    public class APIRequestBinder
    {
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
