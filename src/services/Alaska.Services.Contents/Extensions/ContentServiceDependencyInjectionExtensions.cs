using Alaska.Services.Contents.Infrastructure.Abstractions;
using Alaska.Services.Contents.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Extensions
{
    public static class ContentServiceDependencyInjectionExtensions
    {
        private const string ContentServiceConfigSection = "Alaska:ContentsService";

        public static IContentsServiceBuilder AddContentService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMvcCore()
                .AddApplicationPart(typeof(ContentServiceDependencyInjectionExtensions).Assembly);

            services
                .AddSettings(configuration);

            return new ContentsServiceBuilder(services, configuration);
        }

        private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ContentServiceSettings>(configuration.GetSection(ContentServiceConfigSection));
        }
    }
}
