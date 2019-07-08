using Alaska.Common.Caching.Abstractions;
using Alaska.Common.Collections;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Concrete
{
    internal class MemoryCacheEngine : ICacheEngine
    {
        private SafeList<string> _keys = new SafeList<string>();
        private MemoryCache _cache;

        public MemoryCacheEngine(MemoryCacheOptions options)
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
            object item;
            _cache.TryGetValue(key, out item);
            return item == null ? null : (ICacheItem<T>)item;
        }

        public void Set<T>(ICacheItem<T> item)
        {
            _cache.Set(item.Key, item, DateTimeOffset.FromFileTime(item.ExpirationTime.ToFileTime()));
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
