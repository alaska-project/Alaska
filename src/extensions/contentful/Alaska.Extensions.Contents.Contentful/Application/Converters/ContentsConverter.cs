using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Converters
{
    public class ContentsConverter
    {
        private readonly FieldAdaptersCollection _fieldAdapters;

        public ContentsConverter(FieldAdaptersCollection fieldAdapters)
        {
            _fieldAdapters = fieldAdapters ?? throw new ArgumentNullException(nameof(fieldAdapters));
        }

        public ContentItem ConvertToContentItem(ContentItemData entry, ContentType contentType, PublishingTarget target)
        {
            return new ContentItem
            {
                Fields = GetContentItemFields(entry, contentType),
                Info = GetContentItemInfo(entry, contentType, target),
            };
        }

        public ContentItemData TransformContentItem(ContentItemData originalItem, ContentItem newValues, ContentType contentType)
        {
            contentType.Fields
                .ToList()
                .ForEach(x => SetField(originalItem, x.Id, GetFieldValue(originalItem, newValues, x)));
            return originalItem;
        }

        public ContentItemData TransformContentItemData(ContentItemData originalItem, ContentItemFields newValues, ContentType contentType)
        {
            contentType.Fields
                .ToList()
                .ForEach(x => SetField(originalItem, x.Id, GetFieldValue(originalItem, newValues, x)));
            return originalItem;
        }

        private void SetField(ContentItemData item, string fieldId, dynamic fieldValue)
        {
            if (!item.ContainsKey(fieldId))
                item.Add(fieldId, fieldValue);
            else
                item[fieldId] = fieldValue;
        }

        private ContentItemFields GetContentItemFields(ContentItemData entry, ContentType contentType)
        {
            var fields = new ContentItemFields();
            contentType.Fields
                .ToList()
                .ForEach(x => fields.Add(x.Id, AdaptField(entry, x)));
            return fields;
        }

        private dynamic GetFieldValue(ContentItemData currentItem, ContentItem entry, Field field)
        {
            return GetFieldValue(currentItem, entry.Fields, field);
        }

        private dynamic GetFieldValue(ContentItemData currentItem, ContentItemFields entry, Field field)
        {
            return _fieldAdapters.ResolveAdapter(field.Type).WriteField(currentItem.GetField(field.Id), field, entry.GetField(field.Id));
        }

        private ContentItemField AdaptField(ContentItemData entry, Field field)
        {
            return _fieldAdapters.ResolveAdapter(field.Type).ReadField(entry.GetField(field.Id), field);
        }

        private ContentItemInfo GetContentItemInfo(ContentItemData entry, ContentType contentType, PublishingTarget target)
        {
            return new ContentItemInfo
            {
                Id = GetContentItemId(entry),
                Language = GetContentItemLanguage(entry),
                TemplateId = contentType.SystemProperties.Id,
                TemplateName = contentType.Name,
                PublishingTarget = target.ToString(),
            };
        }

        private string GetContentItemId(ContentItemData entry) => entry["sys"].id;
        private string GetContentItemLanguage(ContentItemData entry) => entry["sys"].locale;
    }
}
