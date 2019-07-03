using Alaska.Extensions.Contents.Contentful.Services;
using Alaska.Extensions.Contents.Contentful.Settings;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Extensions
{
    public static class AlaskaContentfulDependencyInjectionExtensions
    {
        private const string ContentfulSectionName = "Alaska:Contents:Contentful";

        public static IServiceCollection AddContentfulModule(this IContentsServiceBuilder services)
        {
            return services.Services
                .Configure<ContentfulClientOptions>(services.Configuration.GetSection(ContentfulSectionName))
                .AddSingleton<ContentfulClientsFactory>()
                .AddSingleton<FieldAdaptersCollection>()
                .AddScoped<ContentsConverter>()
                .AddScoped<IContentsService, ContentsService>();
        }
    }
}
