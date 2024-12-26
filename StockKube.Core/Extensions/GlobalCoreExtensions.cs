using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StockKube.Core.Cache;
using System.Security.Authentication;

namespace StockKube.Core.Extensions
{
    public static class GlobalCoreExtensions
    {
        public static void InitCoreServices(this IServiceCollection service)
        {
            service.AddSingleton<ICacheAppSettingService, CacheAppSettingService>();
        }

        public static void InitDBServices(this IServiceCollection service, string connString)
        {

            var settings = MongoClientSettings.FromUrl(new MongoUrl(connString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            service.AddSingleton<IMongoClient>(new MongoClient(settings));
        }
    }
}
