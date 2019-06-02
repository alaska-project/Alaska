using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models
{
    public class ContentItem
    {
        public ContentItemInfo Info { get; set; }
        public ContentItemFields Fields { get; set; }
    }
}
