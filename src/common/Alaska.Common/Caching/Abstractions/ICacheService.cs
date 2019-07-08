using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Abstractions
{
    public interface ICacheService
    {
        ICacheInstance GetCache(string cacheId);
        TCacheInstance GetCache<TCacheInstance>() where TCacheInstance : ICacheInstance;
        IEnumerable<ICacheInstance> GetAllCaches();
    }
}
