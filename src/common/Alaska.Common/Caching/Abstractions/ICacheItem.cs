using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Abstractions
{
    public interface ICacheItem : ICacheItemInfo
    {
        object Value { get; }
    }

    public interface ICacheItem<T> : ICacheItem
    {
        new T Value { get; }
    }
}
