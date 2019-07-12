using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Media
{
    public class MediaContent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
