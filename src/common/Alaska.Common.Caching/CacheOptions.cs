using Alaska.Common.Caching.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Caching
{
    public class CacheOptions : ICacheOptions
    {
        public TimeSpan DefaultExpiration { get; set; } = TimeSpan.FromMinutes(30);

        public bool IsDisabled { get; set; } = false;

        public bool CacheNullItems { get; set; } = false;

        public string ConnectionString { get; set; }

        public CacheExpirationMode CacheExpirationMode { get; set; } = CacheExpirationMode.Physical;
    }
}
