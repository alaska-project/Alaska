using Alaska.Common.Caching.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Common.Caching
{
    public class CacheOptions : ICacheOptions
    {
        public TimeSpan DefaultExpiration { get; set; } = TimeSpan.FromMinutes(30);

        public bool IsDisabled => false;

        public bool CacheNullItems => false;

        public string ConnectionString => throw new NotImplementedException();
    }
}
