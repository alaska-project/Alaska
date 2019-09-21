using Alaska.Extensions.Media.Azure.Application.Converters;
using Alaska.Extensions.Media.Azure.Application.Query;
using Alaska.Extensions.Media.Azure.Application.Services;
using Alaska.Extensions.Media.Azure.Infrastructure.Clients;
using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using Alaska.Extensions.Media.Azure.Infrastructure.Settings;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Extensions
{
    public static class AzureMediaLibraryDependencyInjectionExtensions
    {
        private const string AzureMediaSectionName = "Alaska:Media:Azure";

        public static IContentsServiceBuilder AddAzureMediaLibrary(this IContentsServiceBuilder services)
        {
            services.Services
                .AddMediatR(typeof(AzureMediaLibraryService))
                .Configure<AzureMediaStorageOptions>(services.Configuration.GetSection(AzureMediaSectionName))
                .AddScoped<IMediaLibraryProvider, AzureMediaLibraryService>()
                .AddScoped<AzureStorageClientFactory>()
                .AddScoped<AzureMediaLibraryQuery>()
                .AddScoped<AzureStorageRepository>()
                .AddScoped<MediaContentConverter>()
                .AddScoped<MediaFolderConverter>();
            return services;
        }
    }
}
