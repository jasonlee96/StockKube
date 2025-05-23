using MongoDB.Driver;

namespace DAL.Mongo.Helpers
{
    public abstract class MongoHelper<TDocument>
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<TDocument> _collection;
        public IQueryable<TDocument> _query { get { return _collection.AsQueryable(); } }

        public MongoHelper(IMongoClient client, string dbName, string collectionName)
        {
            _client = client;
            _collection = client.GetDatabase(dbName).GetCollection<TDocument>(collectionName);
        }
    }
}
