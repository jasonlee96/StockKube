using Newtonsoft.Json;
using StockKube.Core.Models.IntraObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockKube.Core.Extensions
{
    public static class CommonExtensions
    {
        public static string ToJson(this object obj)
        {
            if (obj == null) return "";

            return JsonConvert.SerializeObject(obj);
        }

        public static T JsonToObj<T>(this string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            else return default;
        }
        public static bool IsNull<T>(this T obj)
        {
            if (obj == null) return true;
            return false;
        }
        public static bool IsNotNull<T>(this T obj)
        {
            if(obj == null) return false;
            return true;
        }
        public static T MustNotNull<T>(this T obj)
        {
            if (obj.IsNotNull()) return obj;
            throw new NullReferenceException(typeof(T).Name);
        }
        public static T DeserializeToPoco<T>(this string json)
        {
            // Ensure inputs are valid
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));
            }

            // Deserialize JSON to the resolved type
            return JsonConvert.DeserializeObject<T>(json)
                   ?? throw new InvalidOperationException($"Failed to deserialize JSON to type '{typeof(T)}'.");
        }
        public static object DeserializeToPoco(this string json, string typeFullName)
        {
            // Ensure inputs are valid
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty.", nameof(json));
            }

            if (string.IsNullOrWhiteSpace(typeFullName))
            {
                throw new ArgumentException("Type full name cannot be null or empty.", nameof(typeFullName));
            }

            // Resolve the type
            var targetType = Type.GetType(typeFullName);
            if (targetType == null)
            {
                throw new ArgumentException($"Type with full name '{typeFullName}' could not be found.", nameof(typeFullName));
            }

            // Deserialize JSON to the resolved type
            return JsonConvert.DeserializeObject(json, targetType)
                   ?? throw new InvalidOperationException($"Failed to deserialize JSON to type '{typeFullName}'.");
        }


        #region Common API Client header binding

        public static void BindHTTP(this HttpClient client, APIRequestBinder binder)
        {
            if (binder.IsNull()) return;

            client.BaseAddress = new Uri(binder.Url);
            foreach (var item in binder.Headers)
            {
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
            
        }
        #endregion
    }
}
