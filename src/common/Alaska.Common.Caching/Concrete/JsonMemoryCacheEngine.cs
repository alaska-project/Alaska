using Alaska.Common.Caching.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alaska.Common.Caching.Model;
using Microsoft.Extensions.Caching.Memory;
using Alaska.Common.Collections;

namespace Alaska.Common.Caching.Concrete
{
    public class JsonMemoryCacheEngine : ICacheEngine
    {
        private SafeList<string> _keys = new SafeList<string>();
        private MemoryCache _cache;

        public JsonMemoryCacheEngine(MemoryCacheOptions options)
        {
            _cache = new MemoryCache(options);
        }

        public ICacheItem Get(string key)
        {
            var item = _cache.Get(key);
            return item == null ? null : (ICacheItem)item;
        }

        public ICacheItem<T> Get<T>(string key)
        {
            var item = _cache.Get(key);
            return item == null ? 
                null : 
                new CacheItem<T>((ICacheItem<T>)item);
        }

        public void Set<T>(ICacheItem<T> item)
        {
            var serializedItem = new SerializedCacheItem<T>(item);
            _cache.Set(item.Key, serializedItem, DateTimeOffset.FromFileTime(item.ExpirationTime.ToFileTime()));
            _keys.Add(item.Key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _keys.Remove(key);
        }

        public void Clear()
        {
            Keys.ToList().ForEach(x => Remove(x));
        }

        public IEnumerable<string> Keys =>
            _keys.Values.ToList();
    }
}
