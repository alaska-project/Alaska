using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Abstractions
{
    public interface ICacheItemInfo
    {
        string Key { get; }
        TimeSpan Expiration { get; }
        DateTime ExpirationTime { get; }
    }
}
