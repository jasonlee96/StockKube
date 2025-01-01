using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StockKube.Core.Extensions;
using StockKube.Core.Models.IntraObjects;
using System.Text;

namespace StockKube.Core.Services.Http
{
    public class HTTPClientService : IHTTPClientService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpFactory;
        public HTTPClientService(ILogger<HTTPClientService> logger) 
        {
            _logger = logger;
        }

        public async Task<TResponse?> GetAsync<TResponse>(string url, Dictionary<string, string> queryParams = null, APIRequestBinder binder = null)
        {
            try
            {
                using var client = _httpFactory.CreateClient();

                client.BindHTTP(binder);

                var resp = await client.GetAsync(AppendQueryParams(url, queryParams));

                resp.EnsureSuccessStatusCode();

                string responseBody = await resp.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(responseBody) ? default : JsonConvert.DeserializeObject<TResponse>(responseBody);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return default;
            }
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest body, APIRequestBinder binder = null)
        {
            try
            {

                using var client = _httpFactory.CreateClient();

                client.BindHTTP(binder);

                using var httpContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

                var resp = await client.PostAsync(url, httpContent);

                resp.EnsureSuccessStatusCode();

                string responseBody = await resp.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(responseBody) ? default : JsonConvert.DeserializeObject<TResponse>(responseBody);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                return default;
            }
        }

        private string AppendQueryParams(string url, Dictionary<string, string> queryParams)
        {
            if (queryParams.IsNull()) return url;
            url += "?";
            foreach(var item in queryParams)
            {
                url += item.Key + "=" + item.Value + "&";
            }

            return url.Substring(0, url.Length - 2); // remove last &

        }
    }
}
