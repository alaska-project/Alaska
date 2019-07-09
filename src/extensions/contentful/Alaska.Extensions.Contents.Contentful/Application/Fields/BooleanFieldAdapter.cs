using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Fields
{
    internal class BooleanFieldAdapter : IFieldAdapter
    {
        public ContentItemField ReadField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Bool,
                Value = field == null ? false : (bool)field,
            };
        }

        public dynamic WriteField(dynamic field, Field fieldDefinition, ContentItemField fieldValue)
        {
            return (bool)fieldValue.Value;
        }
    }
}
