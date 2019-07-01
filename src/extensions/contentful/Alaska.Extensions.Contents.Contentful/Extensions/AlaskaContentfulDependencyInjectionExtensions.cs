using Alaska.Extensions.Contents.Contentful.Services;
using Alaska.Extensions.Contents.Contentful.Settings;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Extensions
{
    public static class AlaskaContentfulDependencyInjectionExtensions
    {
        private const string ContentfulSectionName = "Alaska:Contents:Contentful";

        public static IServiceCollection AddAlaskaContentfulModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ContentfulClientOptions>(ContentfulSectionName, configuration)
                .AddSingleton<ContentsClientFactory>()
                .AddScoped<ContentsConverter>()
                .AddScoped<IContentsService, ContentsService>();
        }
    }
}
