using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Fields
{
    internal class RichTextFieldAdapter : IFieldAdapter
    {
        public ContentItemField ReadField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Html,
                Value = field?.ToString(),
            };
        }

        public dynamic WriteField(dynamic field, Field fieldDefinition, ContentItemField fieldValue)
        {
            return (string)fieldValue.Value;
        }
    }
}
