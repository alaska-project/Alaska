using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Fields
{
    public class ItemLinkField
    {
        [JsonProperty("linkType")]
        public string LinkType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
