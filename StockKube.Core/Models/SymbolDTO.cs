using StockKube.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Models
{
    public class SymbolDTO
    {
        public ExchangeTypeEnum ExchangeType { get; set; }
        public string Symbol { get; set; }
    }
}
