using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Publishing
{
    public enum PublishScope { Item, ItemAndDescendants }

    public class PublishContentRequest
    {
        [JsonProperty("itemId")]
        public string ItemId { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("language")]
        public List<string> Language { get; set; }

        [JsonProperty("scope")]
        public PublishScope Scope { get; set; }
    }
}
