using Alaska.Common.Caching.Concrete;
using Alaska.Common.Caching.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Extensions
{
    public static class InitializationExtensions
    {
        public static IServiceCollection AddJsonMemoryCacheInstance<TCacheInstance>(this IServiceCollection services, Action<ICacheOptions> setupAction = null)
            where TCacheInstance : CacheInstance, new()
        {
            return services.AddCacheInstance<TCacheInstance>(new JsonMemoryCacheEngine(new MemoryCacheOptions()), setupAction);
        }

        public static IServiceCollection AddMemoryCacheInstance<TCacheInstance>(this IServiceCollection services, Action<ICacheOptions> setupAction = null)
            where TCacheInstance : CacheInstance, new()
        {
            return services.AddMemoryCacheInstance<TCacheInstance, TCacheInstance>(setupAction);
        }

        public static IServiceCollection AddMemoryCacheInstance<TService, TCacheInstance>(this IServiceCollection services, Action<ICacheOptions> setupAction = null)
            where TCacheInstance : CacheInstance, TService, new()
            where TService : class
        {
            return services.AddCacheInstance<TService, TCacheInstance>(new MemoryCacheEngine(new MemoryCacheOptions()), setupAction);
        }

        public static IServiceCollection AddCacheInstance<TCacheInstance>(this IServiceCollection services, ICacheEngine cacheEngine, Action<ICacheOptions> setupAction = null)
            where TCacheInstance : CacheInstance, new()
        {
            return services.AddCacheInstance<TCacheInstance, TCacheInstance>(cacheEngine, setupAction);
        }

        public static IServiceCollection AddCacheInstance<TService, TCacheInstance>(this IServiceCollection services, ICacheEngine cacheEngine, Action<ICacheOptions> setupAction = null)
            where TCacheInstance : CacheInstance, TService, new()
            where TService : class
        {
            var options = new CacheOptions();
            setupAction?.Invoke(options);

            var cacheInstance = Activator.CreateInstance<TCacheInstance>();
            cacheInstance.SetCacheEngine(cacheEngine);
            cacheInstance.SetCacheOptions(options);

            return services
                .AddSingleton<TService>(cacheInstance)
                .AddSingleton<ICacheInstance>(cacheInstance)
                .AddCacheService();
        }

        public static IServiceCollection AddCacheService(this IServiceCollection services)
        {
            return services.AddScoped<ICacheService, CacheService>();
        }
    }
}
