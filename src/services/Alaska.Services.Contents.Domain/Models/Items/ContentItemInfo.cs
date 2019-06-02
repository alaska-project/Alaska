using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Items
{
    public class ContentItemInfo
    {
        public string Id { get; set; }
        public string TemplateId { get; set; }
        public List<string> Path { get; set; }
        public List<string> IdPath { get; set; }
    }
}
