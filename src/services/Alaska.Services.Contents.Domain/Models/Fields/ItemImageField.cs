using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Fields
{
    public class ItemImageField
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }
    }
}
