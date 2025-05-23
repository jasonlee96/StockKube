using DAL.Mongo.Constants;
using DAL.Mongo.Helpers;
using DAL.Mongo.Models;
using DAL.Mongo.Repositories.Interfaces;
using MongoDB.Driver;

namespace DAL.Mongo.Repositories
{
    public class StockRateRepository : MongoHelper<StockRate>, IStockRateRepository
    {
        public StockRateRepository(IMongoClient client) : base(client, DBConstants.DB_NAME, DBConstants.COL_STOCKRATE_NAME)
        {

        }

        public List<StockRate> GetRateBySymbol(string symbol)
        {
            return _query.Where(x => x.Symbol.ToUpper() == symbol.ToUpper()).ToList();
        }
    }
}
