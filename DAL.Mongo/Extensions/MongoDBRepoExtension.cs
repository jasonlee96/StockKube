using DAL.Mongo.Repositories;
using DAL.Mongo.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mongo.Extensions
{
    public static class MongoDBRepoExtension
    {
        public static void InitRepositories(this IServiceCollection service, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var allProviderTypes = System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => t.Namespace != null && t.Namespace.Contains("Repository"));

            foreach (var intfc in allProviderTypes.Where(t => t.IsInterface))
            {
                var impl = allProviderTypes.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null) {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            service.AddSingleton(intfc, impl);
                            break;
                        case ServiceLifetime.Transient:
                            service.AddTransient(intfc, impl);
                            break;
                        default:
                            service.AddScoped(intfc, impl);
                            break;
                    }
                };
            }
        }
    }
}
