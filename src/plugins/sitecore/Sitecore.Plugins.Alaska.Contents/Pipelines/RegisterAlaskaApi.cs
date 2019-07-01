using Sitecore.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace Sitecore.Plugins.Alaska.Contents.Pipelines
{
    public class RegisterAlaskaApi
    {
        public void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configuration.EnsureInitialized();
            GlobalConfiguration.Configure(Configure);
        }

        protected void Configure(HttpConfiguration configuration)
        {
            var routes = configuration.Routes;
            routes.MapHttpRoute("AlaskaApi", "alaska/api/{controller}/{action}");
        }
    }
}
