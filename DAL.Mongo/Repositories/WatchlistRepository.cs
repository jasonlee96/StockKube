using DAL.Mongo.Constants;
using DAL.Mongo.Helpers;
using DAL.Mongo.Models;
using DAL.Mongo.Repositories.Interfaces;
using MongoDB.Driver;

namespace DAL.Mongo.Repositories
{
    public class WatchlistRepository : MongoHelper<Watchlist>, IWatchlistRepository
    {
        public WatchlistRepository(IMongoClient client) : base(client, DBConstants.DB_NAME, DBConstants.COL_WATCHLIST_NAME)
        {
        }

        public Task<List<Watchlist>> GetAllWatchlistAsync()
        {
            return Task.FromResult(FormQueryable().ToList());
        }
    }
}
