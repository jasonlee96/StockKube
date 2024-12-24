using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Cache
{
    public class CacheAppSettingService : ICacheAppSettingService
    {
        public CacheAppSettingService() { 
        }

        public Task<T> GetSettingAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task PreloadOrReloadAllSetting()
        {
            // r&d a pipeline to  fetch all the keys

            throw new NotImplementedException();
        }
    }
}
