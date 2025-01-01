using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mongo.Models
{
    public class StockRate : BaseModel
    {
        public ObjectId Id { get; set; }
        [BsonElement("symbol")]
        public required string Symbol { get; set; }
        [BsonElement("rate")]
        public double Rate { get; set; }
        [BsonElement("lastRetrieveAt")]
        public DateTime? LastRetrieveAt { get; set; }
    }
}
