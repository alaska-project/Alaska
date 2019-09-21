using Alaska.Services.Contents.Application.Queries;
using Alaska.Services.Contents.Application.Services;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Alaska.Services.Contents.Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("MediatR")]
[assembly: InternalsVisibleTo("MediatR.Extensions.Microsoft.DependencyInjection")]
namespace Alaska.Services.Contents.Extensions
{
    public static class ContentServiceDependencyInjectionExtensions
    {
        private const string ContentServiceConfigSection = "Alaska:ContentsService";

        public static IContentsServiceBuilder AddContentService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMediatR(typeof(ContentServiceDependencyInjectionExtensions))
                .AddScoped<IContentsService, ContentsService>()
                .AddScoped<IContentQueries, ContentQueries>()
                .AddScoped<IMediaLibraryService, MediaLibraryService>()
                .AddScoped<IMediaQueries, MediaQueries>()
                .AddSingleton<IImageHelper, DefaultImageHelper>()
                .AddSingleton<IImageTransformer, DefaultImageTransformer>()
                .AddSettings(configuration);

            return new ContentsServiceBuilder(services, configuration);
        }

        public static IContentsServiceBuilder AddContentsApi(this IContentsServiceBuilder services)
        {
            services.Services
                .AddMvcCore()
                .AddApplicationPart(typeof(ContentServiceDependencyInjectionExtensions).Assembly);

            return services;
        }

        public static IContentsServiceBuilder AddContentsAuthorization<T>(this IContentsServiceBuilder services)
            where T : class, IContentsAuthorizationMiddleware
        {
            services.Services.AddScoped<IContentsAuthorizationMiddleware, T>();
            return services;
        }

        public static IContentsServiceBuilder AddMediaAuthorization<T>(this IContentsServiceBuilder services)
            where T : class, IMediaLibraryAuthorizationMiddleware
        {
            services.Services.AddScoped<IMediaLibraryAuthorizationMiddleware, T>();
            return services;
        }

        private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ContentServiceSettings>(configuration.GetSection(ContentServiceConfigSection));
        }
    }
}
