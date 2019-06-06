using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Items
{
    public class ContentItem
    {
        [JsonProperty("info")]
        public ContentItemInfo Info { get; set; }

        [JsonProperty("fields")]
        public ContentItemFields Fields { get; set; }
    }
}
