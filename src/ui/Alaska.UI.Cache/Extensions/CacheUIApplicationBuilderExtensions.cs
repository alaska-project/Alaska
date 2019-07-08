using Alaska.UI.Cache.Extensions;
using Alaska.UI.Cache.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class CacheUIApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseAlaskaCacheUI(this IApplicationBuilder app, Action<CacheUIOptions> setupAction = null)
        {
            var options = new CacheUIOptions();
            setupAction?.Invoke(options);

            CacheUIOptionsRepository.Options = options;

            app.UseMiddleware<CacheUIIndexMiddleware>(options);
            //app.UseFileServer(new FileServerOptions
            //{
            //    RequestPath = string.IsNullOrEmpty(options.RoutePrefix) ? string.Empty : $"/{options.RoutePrefix}",
            //    FileProvider = new EmbeddedFileProvider(typeof(SwaggerUIBuilderExtensions).GetTypeInfo().Assembly, EmbeddedFilesNamespace),
            //});

            return app;
        }
    }
}
