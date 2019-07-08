using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Abstractions
{
    public interface ICacheEngine
    {
        ICacheItem Get(string key);

        ICacheItem<T> Get<T>(string key);

        void Set<T>(ICacheItem<T> item);

        void Remove(string key);

        void Clear();

        IEnumerable<string> Keys { get; }
    }
}
