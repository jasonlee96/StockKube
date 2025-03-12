using DAL.Mongo.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using StockKube.Core.Cache;
using StockKube.Core.ExternalSources;
using StockKube.Core.ExternalSources.Crypto;
using StockKube.Core.ExternalSources.KLSE;
using System.Security.Authentication;

namespace StockKube.Core.Extensions
{
    public static class GlobalCoreExtensions
    {
        public static void InitCoreServices(this IServiceCollection service)
        {
            service.AddSingleton<ICacheAppSettingService, CacheAppSettingService>();

            // SourceData Provider
            service.AddSingleton<SourceDataProviderFactory>();
            service.AddSingleton<ISourceDataProvider, KLSEDataProvider>();
            service.AddSingleton<ISourceDataProvider, CryptoDataProvider>();
        }

        public static void InitDBServices(this IServiceCollection service, string connString)
        {

            var settings = MongoClientSettings.FromUrl(new MongoUrl(connString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            service.AddSingleton<IMongoClient>(new MongoClient(settings));

            var serviceProvider = service.BuildServiceProvider();

            // Run database seeder
            var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
            var seeder = new DatabaseSeeder(mongoClient);
            seeder.ApplyMigrations();
        }
    }
}
