using DAL.Mongo.Repositories;
using DAL.Mongo.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mongo.Extensions
{
    public static class MongoDBRepoExtension
    {
        public static void InitRepositories(this IServiceCollection service)
        {
            service.AddTransient<IStockRateRepository, StockRateRepository>();
            service.AddTransient<IExternalSourceRepository, ExternalSourceRepository>();
        }
    }
}
