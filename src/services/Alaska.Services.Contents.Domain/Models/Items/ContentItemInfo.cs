using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Items
{
    public class ContentItemInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("publishingTarget")]
        public string PublishingTarget { get; set; }

        [JsonProperty("templateId")]
        public string TemplateId { get; set; }

        [JsonProperty("templateName")]
        public string TemplateName { get; set; }

        [JsonProperty("path")]
        public List<string> Path { get; set; }

        [JsonProperty("idPath")]
        public List<string> IdPath { get; set; }
    }
}
