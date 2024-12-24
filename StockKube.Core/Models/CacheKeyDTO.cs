using StockKube.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Models
{
    public class CacheKeyDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DataTypeEnum DataType { get; set; }
    }
}
