using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Cache
{
    public interface ICacheAppSettingService
    {
        public Task PreloadOrReloadAllSetting();
        public Task<T> GetSettingAsync<T>(string key);
    }
}
