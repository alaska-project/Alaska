using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Fields
{
    internal class DateTimeFieldAdapter : IFieldAdapter
    {
        public ContentItemField ReadField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.DateTime,
                Value = field?.ToString(), //ParseDateTime(field?.ToString()),
            };
        }

        public dynamic WriteField(ContentItemField field, Field fieldDefinition)
        {
            return field.Value;
        }

        //private DateTime? ParseDateTime(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        return null;

        //    return DateTime.Parse(value);
        //}
    }
}
