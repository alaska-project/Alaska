using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Media
{
    public class MediaFolder
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
