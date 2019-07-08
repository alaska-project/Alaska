using Alaska.Common.Caching.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Common.Caching.Model
{
    public class CacheItem<T>: ICacheItem<T>
    {
        public CacheItem()
        { }

        public CacheItem(ICacheItem<T> entry)
        {
            Key = entry.Key;
            Value = entry.Value;
            Expiration = entry.Expiration;
            ExpirationTime = entry.ExpirationTime;
        }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("serializedValue")]
        public string SerializedValue => SerializeValue(Value);

        [JsonIgnore]
        public T Value { get; set; }

        [JsonProperty("expiration")]
        public TimeSpan Expiration { get; set; }

        [JsonProperty("expirationTime")]
        public DateTime ExpirationTime { get; set; }

        [JsonProperty("logicalExpirationTime")]
        public DateTime LogicalExpirationTime { get; set; }

        [JsonIgnore]
        object ICacheItem.Value => Value;

        private string SerializeValue(object value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return null;
            }
        }
    }

    public class CacheItem: ICacheItem
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("expiration")]
        public TimeSpan Expiration { get; set; }

        [JsonProperty("expirationTime")]
        public DateTime ExpirationTime { get; set; }

        [JsonProperty("logicalExpirationTime")]
        public DateTime LogicalExpirationTime { get; set; }
    }
}
