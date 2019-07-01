using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Utils
{
    internal static class StringHelpers
    {
        public static string ToCamelCase(string value)
        {
            return value.Length > 0 ?
                value.Substring(0, 1).ToLower() + value.Substring(1) :
                value;
        }
    }
}
