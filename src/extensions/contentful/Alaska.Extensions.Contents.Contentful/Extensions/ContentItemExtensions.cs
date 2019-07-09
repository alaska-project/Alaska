using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Extensions
{
    internal static class ContentItemExtensions
    {
        public static ContentItemReference GetReference(this ContentItem item)
        {
            return new ContentItemReference
            {
                Id = item.Info.Id,
                Locale = item.Info.Language,
            };
        }
    }
}
