using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Abstractions
{
    public interface ICacheInstance
    {
        string CacheId { get; }

        string CacheEngine { get; }

        bool IsDisabled { get; }

        T Retreive<T>(string key, Func<T> initializationExpression);

        T Retreive<T>(string key, Func<T> initializationExpression, TimeSpan expiration);

        T RetreiveExclusive<T>(string key, Func<T> initializationExpression);

        T RetreiveExclusive<T>(string key, Func<T> initializationExpression, TimeSpan expiration);

        bool Get<T>(string key, out T value);

        void Set<T>(string key, T value);

        void Set<T>(string key, T value, TimeSpan expiration);

        void Remove(string key);

        void Clear();

        IEnumerable<string> Keys { get; }

        ICacheItem GetItem(string key);

        ICacheItem<T> GetItem<T>(string key);
    }
}
