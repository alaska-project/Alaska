using Alaska.Common.Caching.Extensions;
using Alaska.Common.Extensions;
using Alaska.Extensions.Contents.Contentful.Application.Query;
using Alaska.Extensions.Contents.Contentful.Converters;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Caching;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Settings;
using Alaska.Extensions.Contents.Contentful.Services;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Extensions
{
    public static class AlaskaContentfulDependencyInjectionExtensions
    {
        private const string ContentfulSectionName = "Alaska:Contents:Contentful";

        public static IContentsServiceBuilder AddContentfulModule(this IContentsServiceBuilder services)
        {
            services.Services
                .AddMediatR(typeof(ContentsService))
                .AddAlaskaCommon()
                .Configure<ContentfulClientOptions>(services.Configuration.GetSection(ContentfulSectionName))
                .AddSingleton<ContentfulClientsFactory>()
                .AddSingleton<FieldAdaptersCollection>()
                .AddScoped<ContentsConverter>()
                .AddScoped<ContentQueries>()
                .AddMemoryCacheInstance<ContentTypesCache>()
                .AddScoped<IContentsService, ContentsService>();

            return services;
        }
    }
}
