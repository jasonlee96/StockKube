using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DAL.Mongo.Constants;
using DAL.Mongo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Extensions.Migration;

namespace DAL.Mongo.Migrations
{
    public class DatabaseSeeder
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<BsonDocument> _versionCollection;

        public DatabaseSeeder(IMongoClient mongoClient)
        {
            _database = mongoClient.GetDatabase(DBConstants.DB_NAME);
            _versionCollection = _database.GetCollection<BsonDocument>(DBConstants.COL_MIGRATION_NAME);
        }

        public void ApplyMigrations()
        {
            var migrations = new List<(int Version, Action<IMongoDatabase> Migration)>
            {
                (1, SeedInitialData),
            };

            foreach (var (version, migration) in migrations)
            {
                if (!IsMigrationApplied(version))
                {
                    migration(_database);
                    MarkMigrationAsApplied(version);
                }
            }
        }

        private bool IsMigrationApplied(int version)
        {
            return _versionCollection.CountDocuments(Builders<BsonDocument>.Filter.Eq("Version", version)) > 0;
        }

        private void MarkMigrationAsApplied(int version)
        {
            var versionDoc = new BsonDocument
        {
            { "Version", version },
            { "AppliedAt", DateTime.UtcNow }
        };
            _versionCollection.InsertOne(versionDoc);
        }

        private void SeedInitialData(IMongoDatabase db)
        {
            // data
            ExternalSource externalSource = new ExternalSource { APIKey = "3qURsGp5gYBH0wN_IcqWeKTh1T5mbeEL", APIUrl = "https://api.polygon.io/", CreatedAt = DateTime.UtcNow, ExchangeType = "CRYPTO", IntervalInMin = 15, UpdatedAt = DateTime.UtcNow };

            var collection = db.GetCollection<ExternalSource>(DBConstants.COL_EXTERNALSOURCE_NAME);

            if (collection.CountDocuments(FilterDefinition<ExternalSource>.Empty) == 0)
            {
                collection.InsertOne(externalSource);
            }
        }
    }
}
