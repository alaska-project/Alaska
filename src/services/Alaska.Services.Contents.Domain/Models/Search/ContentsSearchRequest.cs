using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Search
{
    public enum FieldFilterOperator { Equals, NotEquals, Matches }

    public class ContentsSearchRequest
    {
        [JsonProperty("filters")]
        public ContentItemFieldsFilter Filters { get; set; }

        [JsonProperty("publishingTarget")]
        public string PublishingTarget { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class ContentItemFieldsFilter : List<ContentItemFieldFilter>
    {
    }

    public class ContentItemFieldFilter
    {
        [JsonProperty("operator")]
        public FieldFilterOperator Operator { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
