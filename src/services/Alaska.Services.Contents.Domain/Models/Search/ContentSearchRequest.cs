using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Search
{
    public enum ContentsSearchDepth { Item, Children, Descendants }

    public class ContentsSearchRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("depth")]
        public ContentsSearchDepth Depth { get; set; }

        [JsonProperty("publishingTarget")]
        public string PublishingTarget { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
