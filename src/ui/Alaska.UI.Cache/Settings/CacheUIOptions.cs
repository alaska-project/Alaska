using Alaska.Common.Web.Middleware;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Alaska.UI.Cache.Settings
{
    public class CacheUIOptions : IWebUIOptions
    {
        public string RoutePrefix { get; set; } = "alaska/cache";

        public string ManifestResourceBasePath { get; set; } = "Alaska.UI.Cache.angular.dist.alaska_cache";

        public Assembly ManifestResourceAssembly { get; set; } = typeof(CacheUIOptions).GetTypeInfo().Assembly;
        
        public List<string> Endpoints { get; set; } = new List<string>();

        public string DocumentTitle { get; set; } = "Alaska Cahe UI";

        public string HeadContent { get; set; } = "";

        public JObject ConfigObject { get; } = JObject.FromObject(new
        {
            urls = new object[] { },
            validatorUrl = JValue.CreateNull()
        });

        public JObject OAuthConfigObject { get; } = JObject.FromObject(new
        {
        });
    }
}
