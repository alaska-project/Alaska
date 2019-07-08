using Alaska.Common.Web.Middleware;
using Alaska.UI.Cache.Settings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Alaska.UI.Cache.Extensions
{
    public class CacheUIIndexMiddleware : WebUIPluginMiddleware
    {
        public CacheUIIndexMiddleware(RequestDelegate next, CacheUIOptions options)
            : base(next, options)
        {
        }
    }
}
