using StockKube.Core.Enums;

namespace StockKube.Core.Models
{
    public class CacheKeyDTO
    {
        public string ClassName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DataTypeEnum DataType { get; set; }
    }
}
