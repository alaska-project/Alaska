using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Domain.Models.Publishing
{
    public enum PublishScope { Item, ItemAndDescendants }

    public class PublishContentRequest
    {
        public string ItemId { get; set; }
        public string Target { get; set; }
        public PublishScope Scope { get; set; }
    }
}
