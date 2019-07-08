using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Abstractions
{
    public interface ICacheOptions
    {
        TimeSpan DefaultExpiration { get; }
        bool IsDisabled { get; }
        bool CacheNullItems { get; }
        string ConnectionString { get; }
    }
}
