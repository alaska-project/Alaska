using Alaska.Services.Contents.Domain.Models.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Requests
{
    public class ContentCreationRequest
    {
        [JsonProperty("templateId")]
        public string TemplateId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("parentId")]
        public string ParentId { get; set; }

        [JsonProperty("fields")]
        public ContentItemFields Fields { get; set; }
    }
}
