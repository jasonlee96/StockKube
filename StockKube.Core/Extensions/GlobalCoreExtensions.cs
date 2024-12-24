using Microsoft.Extensions.DependencyInjection;
using StockKube.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Extensions
{
    public static class GlobalCoreExtensions
    {
        public static void InitRepositories(this IServiceCollection service)
        {
            service.AddSingleton<ICacheAppSettingService, CacheAppSettingService>();
        }
    }
}
