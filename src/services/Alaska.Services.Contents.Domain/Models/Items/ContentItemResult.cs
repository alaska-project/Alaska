using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Items
{
    public class ContentItemResult
    {
        [JsonProperty("value")]
        public ContentItem Value { get; set; }

        [JsonProperty("children")]
        public List<ContentItemResult> Children { get; set; }
    }
}
