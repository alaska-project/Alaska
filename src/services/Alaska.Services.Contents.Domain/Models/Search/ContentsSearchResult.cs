﻿using Alaska.Services.Contents.Domain.Models.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Search
{
    public class ContentsSearchResult
    {
        [JsonProperty("items")]
        public IEnumerable<ContentItem> Items { get; set; }
    }
}
