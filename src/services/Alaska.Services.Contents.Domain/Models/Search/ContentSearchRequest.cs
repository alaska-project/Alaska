using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Search
{
    public enum ContentsSearchDepth { Item, Children, Descendants }

    public class ContentSearchRequest
    {
        private const ContentsSearchDepth DefaultDepth = ContentsSearchDepth.Item;

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("depth")]
        public string Depth { get; set; }

        public ContentsSearchDepth GetDepth()
        {
            if (string.IsNullOrEmpty(Depth))
                return DefaultDepth;

            return (ContentsSearchDepth)Enum.Parse(typeof(ContentsSearchDepth), Depth, true);
        }

        [JsonProperty("publishingTarget")]
        public string PublishingTarget { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }
}
