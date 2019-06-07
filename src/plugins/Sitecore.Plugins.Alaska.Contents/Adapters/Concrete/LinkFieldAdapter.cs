using Alaska.Services.Contents.Domain.Models.Fields;
using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Adapters.Concrete
{
    internal class LinkFieldAdapter : FieldAdapter<LinkField>
    {
        public override ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Link,
                Value = new ItemLinkField
                {
                    LinkType = GetField(field)?.LinkType,
                    Target = GetField(field)?.Target,
                    Text = GetField(field)?.Text,
                    Url = GetUrl(field),
                },
            };
        }

        public override void UpdateField(ContentItemField value, Field field)
        {
            var linkField = GetField(field);
            linkField.LinkType = GetLinkValue(value)?.LinkType;
            //TODO
        }

        private string GetUrl(LinkField link)
        {
            switch (link.LinkType.ToLower())
            {
                case "internal":
                    return link.TargetItem != null ? Sitecore.Links.LinkManager.GetItemUrl(link.TargetItem) : null;
                case "media":
                    return link.TargetItem != null ? Sitecore.Resources.Media.MediaManager.GetMediaUrl(link.TargetItem) : null;
                case "anchor":
                    return $"#{link.Url}";
                case "external":
                default:
                    return link.Url;
            }
        }

        private ItemLinkField GetLinkValue(ContentItemField value)
        {
            return (ItemLinkField)value?.Value;
        }

        public override LinkField GetField(Field field)
        {
            return (LinkField)field;
        }
    }
}
