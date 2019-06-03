using Alaska.Services.Contents.Domain.Models.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Fields
{
    public class LinkedItemField
    {
        [JsonProperty("item")]
        public ContentItem Item { get; set; }
    }
}
