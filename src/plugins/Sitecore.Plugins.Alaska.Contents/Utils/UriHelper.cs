using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Utils
{
    internal static class UriHelper
    {
        public static bool IsAbsoluteUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        public static string BuildAbsoluteUrl(string baseUrl, string relativeUrl)
        {
            return new Uri(new Uri(baseUrl, UriKind.Absolute), relativeUrl).AbsoluteUri;
        }
    }
}
