using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Models
{
    internal enum PublishingTarget { Preview, Web }

    internal class ContentItemReference
    {
        public string Id { get; set; }
        public string Locale { get; set; }
    }
}
