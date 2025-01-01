using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mongo.Models
{
    public class ExternalSource : BaseModel
    {
        public ObjectId Id { get; set; }
        [BsonElement("exchangeType")]
        public required string ExchangeType { get; set; }
        [BsonElement("apiUrl")]
        public string APIUrl { get; set; }
        [BsonElement("apiKey")]
        public string APIKey { get; set; }

        [BsonElement("intervalInMin")]
        public int IntervalInMin { get; set; }
    }
}
