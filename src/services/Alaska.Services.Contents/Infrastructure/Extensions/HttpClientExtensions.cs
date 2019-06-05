using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Extensions
{
    internal static class HttpClientExtensions
    {
        public static async Task<T> GetJson<T>(this HttpClient client, string url)
        {
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public static async Task<T> GetJson<T>(this HttpClient client, Uri url)
        {
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(response);
        }


    }
}
