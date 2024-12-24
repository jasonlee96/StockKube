using DAL.Mongo.Constants;
using DAL.Mongo.Helpers;
using DAL.Mongo.Models;
using DAL.Mongo.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mongo.Repositories
{
    public class ExternalSourceRepository : MongoHelper<ExternalSource>, IExternalSourceRepository
    {
        public ExternalSourceRepository(IMongoClient client) : base(client, DBConstants.DB_NAME, DBConstants.COL_EXTERNALSOURCE_NAME)
        {

        }

        public async Task<List<ExternalSource>> GetAllExternalSourcesAsync()
        {
            return await Task.FromResult(FormQueryable().ToList());
        }
    }
}
