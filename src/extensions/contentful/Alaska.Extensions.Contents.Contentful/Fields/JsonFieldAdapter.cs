using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Fields
{
    internal class JsonFieldAdapter : IFieldAdapter
    {
        public ContentItemField AdaptField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.JsonObject,
                Value = field == null ? null : JsonConvert.SerializeObject(field),
            };
        }
    }
}
