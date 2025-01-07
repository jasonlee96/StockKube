using StockKube.Core.Models.IntraObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Services.Http
{
    public interface IHTTPClientService
    {
        Task<TResponse?> GetAsync<TResponse>(string url, Dictionary<string, string> queryParams = null, APIRequestBinder binder = null);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body, APIRequestBinder binder = null);
    }
}
