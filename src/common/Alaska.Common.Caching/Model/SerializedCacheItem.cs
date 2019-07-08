using Alaska.Common.Caching.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Model
{
    internal class SerializedCacheItem<T>: ICacheItem<T>
    {
        public SerializedCacheItem()
        { }

        public SerializedCacheItem(ICacheItem<T> entry)
        {
            Key = entry.Key;
            SerializedValue = JsonConvert.SerializeObject(entry.Value);
            Expiration = entry.Expiration;
            ExpirationTime = entry.ExpirationTime;
        }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("serializedValue")]
        public string SerializedValue { get; set; }

        [JsonIgnore]
        public T Value => JsonConvert.DeserializeObject<T>(SerializedValue);

        [JsonProperty("expiration")]
        public TimeSpan Expiration { get; set; }

        [JsonProperty("expirationTime")]
        public DateTime ExpirationTime { get; set; }

        [JsonIgnore]
        object ICacheItem.Value => Value;
    }
}
