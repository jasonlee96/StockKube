using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.ExternalSources.Crypto
{
    public class GetRateResponse
    {
        [JsonProperty("adjusted")]
        public bool Adjusted { get; set; }
        [JsonProperty("queryCount")]
        public int QueryCount { get; set; }
        [JsonProperty("resultsCount")]
        public int ResultCount { get; set; }
        public string Status { get; set; }
        public string Ticker { get; set; }
        [JsonProperty("results")]
        public List<GetRateResult> Result { get; set; }
    }

    public class GetRateResult
    {
        [JsonProperty("c")]
        public decimal Close { get; set; }
        [JsonProperty("h")]
        public decimal High{ get; set; }
        [JsonProperty("l")]
        public decimal Low{ get; set; }
        [JsonProperty("n")]
        public int TransactionCount { get; set; }
        [JsonProperty("o")]
        public decimal Open { get; set; }
        [JsonProperty("t")]
        public long Timestamp { get; set; }
        [JsonProperty("v")]
        public decimal Volume { get; set; }
        [JsonProperty("vw")]
        public decimal VolumeWeightedAvgPrice { get; set; }
    }
}
