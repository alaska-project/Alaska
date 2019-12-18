using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Utils
{
    public static class UriHelper
    {
        public static string ReplaceHost(Uri original, string newHostName)
        {
            var builder = new UriBuilder(original)
            {
                Host = newHostName
            };
            return builder.Uri.ToString();
        }
    }
}
