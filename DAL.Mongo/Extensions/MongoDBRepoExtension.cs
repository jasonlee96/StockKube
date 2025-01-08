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
            var allProviderTypes = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("Repository"));

            foreach (var intfc in allProviderTypes.Where(t => t.IsInterface))
            {
                var impl = allProviderTypes.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null) service.AddTransient(intfc, impl);
            }
        }
    }
}
