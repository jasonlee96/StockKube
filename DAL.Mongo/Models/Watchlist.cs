using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DAL.Mongo.Models
{
    public class Watchlist : BaseModel
    {
        public ObjectId Id { get; set; }
        [BsonElement("symbol")]
        public required string Symbol { get; set; }
        [BsonElement("exchangeType")]
        public required string ExchangeType{ get; set; }
    }
}
