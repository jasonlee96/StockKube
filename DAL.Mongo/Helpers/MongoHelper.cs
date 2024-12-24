using MongoDB.Driver;

namespace DAL.Mongo.Helpers
{
    public abstract class MongoHelper<TDocument>
    {
        private readonly IMongoClient _client;
        private readonly IMongoCollection<TDocument> _collection;

        public MongoHelper(IMongoClient client, string dbName, string collectionName)
        {
            _client = client;
            _collection = client.GetDatabase(dbName).GetCollection<TDocument>(collectionName);
        }
        internal IQueryable<TDocument> FormQueryable()
        {
            return _collection.AsQueryable();
        }
    }
}
