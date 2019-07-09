using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Converters
{
    internal class ContentsConverter
    {
        private readonly FieldAdaptersCollection _fieldAdapters;

        public ContentsConverter(FieldAdaptersCollection fieldAdapters)
        {
            _fieldAdapters = fieldAdapters ?? throw new ArgumentNullException(nameof(fieldAdapters));
        }

        public ContentItem ConvertToContentItem(ContentItemData entry, ContentType contentType, string target)
        {
            return new ContentItem
            {
                Fields = GetContentItemFields(entry, contentType),
                Info = GetContentItemInfo(entry, contentType, target),
            };
        }

        public ContentItemData ConvertToContentEntry(ContentItem contentItem, ContentType contentType)
        {
            var content = new ContentItemData();
            contentType.Fields
                .ToList()
                .ForEach(x => content.Add(x.Id, GetFieldValue(contentItem, x)));
            return content;
        }

        private ContentItemFields GetContentItemFields(ContentItemData entry, ContentType contentType)
        {
            var fields = new ContentItemFields();
            contentType.Fields
                .ToList()
                .ForEach(x => fields.Add(x.Id, AdaptField(entry, x)));
            return fields;
        }

        private dynamic GetFieldValue(ContentItem entry, Field field)
        {
            return _fieldAdapters.ResolveAdapter(field.Type).WriteField(entry.Fields[field.Id], field);
        }

        private ContentItemField AdaptField(ContentItemData entry, Field field)
        {
            return _fieldAdapters.ResolveAdapter(field.Type).ReadField(entry.GetField(field.Id), field);
        }

        private ContentItemInfo GetContentItemInfo(ContentItemData entry, ContentType contentType, string target)
        {
            return new ContentItemInfo
            {
                Id = GetContentItemId(entry),
                Language = GetContentItemLanguage(entry),
                TemplateId = contentType.SystemProperties.Id,
                TemplateName = contentType.Name,
                PublishingTarget = target,
            };
        }

        private string GetContentItemId(ContentItemData entry) => entry["sys"].id;
        private string GetContentItemLanguage(ContentItemData entry) => entry["sys"].locale;
    }
}
