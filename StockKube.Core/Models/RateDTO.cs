using StockKube.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Models
{
    public class RateDTO
    {
        public ExchangeTypeEnum ExchangeType { get; set; }
        public string Symbol { get; set; }
        public decimal Rate { get; set; }

    }
}
