using Alaska.UI.Cache.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.UI.Cache.Extensions
{
    public static class CacheUIServicesExtensions
    {
        public static IServiceCollection AddCacheUI(this IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddApplicationPart(typeof(CacheUIController).Assembly);

            return services;
        }
    }
}
