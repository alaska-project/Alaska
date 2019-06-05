using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Utils
{
    internal static class UriHelper
    {
        public static Uri Build(string baseUrl, IDictionary<string, object> queryParameters)
        {
            var parameters = queryParameters
                .Where(x => x.Value != null)
                .Select(x => $"{x.Key}={WebUtility.UrlEncode(x.Value.ToString())}")
                .ToList();

            return new Uri($"{baseUrl}?{string.Join("&", parameters)}", UriKind.RelativeOrAbsolute);
        }

        public static Uri Build(string baseUrl, NameValueCollection queryParameters)
        {
            var parameters = new List<string>();
            foreach (var key in queryParameters.AllKeys)
                parameters.AddRange(queryParameters.GetValues(key).Select(x => $"{key}={WebUtility.UrlEncode(x)}"));

            return new Uri($"{baseUrl}?{string.Join("&", parameters)}", UriKind.RelativeOrAbsolute);
        }
    }
}
