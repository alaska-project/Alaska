using Alaska.Extensions.Media.Azure.Application.Query;
using Alaska.Extensions.Media.Azure.Application.Services;
using Alaska.Extensions.Media.Azure.Infrastructure.Clients;
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
        public static IServiceCollection AddAzureMediaLibrary(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(AzureMediaLibraryService))
                .AddScoped<IMediaLibraryService, AzureMediaLibraryService>()
                .AddScoped<AzureStorageClientFactory>()
                .AddScoped<AzureMediaLibraryQuery>();
        }
    }
}
