using Alaska.Common.Caching.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Settings
{
    public class CacheSettings : ICacheOptions
    {
        [JsonProperty("defaultExpiration")]
        public TimeSpan DefaultExpiration { get; set; }

        [JsonProperty("isDisabled")]
        public bool IsDisabled { get; set; }

        [JsonProperty("cacheNullItems")]
        public bool CacheNullItems { get; set; }

        [JsonIgnore]
        public Type CacheEngineType { get; set; }

        [JsonProperty("cacheEngineType")]
        public string CacheEngineTypeStr
        {
            get => CacheEngineType.FullName;
            set => CacheEngineType = Type.GetType(value);
        }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
    }
}
