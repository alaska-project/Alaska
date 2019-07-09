using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Models
{
    public enum PublishingTarget { Preview, Web }

    public class ContentItemReference
    {
        public string Id { get; set; }
        public string Locale { get; set; }
    }
}
