using Alaska.Common.Caching.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching
{
    internal class CacheService : ICacheService
    {
        private IServiceProvider _services;

        public CacheService(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public ICacheInstance GetCache(string cacheId)
        {
            return GetAllCaches().FirstOrDefault(x => x.CacheId.Equals(cacheId));
        }

        public TCacheInstance GetCache<TCacheInstance>()
            where TCacheInstance : ICacheInstance
        {
            return _services.GetRequiredService<TCacheInstance>();
        }

        public IEnumerable<ICacheInstance> GetAllCaches()
        {
            return _services.GetServices<ICacheInstance>();
        }
    }
}
