using Alaska.Extensions.Contents.Contentful.Abstractions;
using Alaska.Extensions.Contents.Contentful.Utils;
using Alaska.Services.Contents.Domain.Models.Fields;
using Alaska.Services.Contents.Domain.Models.Items;
using Contentful.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Fields
{
    internal class CoordinatesFieldAdapter : IFieldAdapter
    {
        public ContentItemField ReadField(dynamic field, Field fieldDefinition)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Coordinates,
                Value = field == null ? null : new CoordinatesField
                {
                    Latitude = field.lat.Value,
                    Longitude = field.lon.Value,
                },
            };
        }

        public dynamic WriteField(dynamic field, Field fieldDefinition, ContentItemField fieldValue)
        {
            CoordinatesField coordinatesField = FieldSerializationUtil.ConvertDeserializedField<CoordinatesField>(fieldValue.Value);
            var newField = FieldSerializationUtil.JsonClone<dynamic>(field);
            newField.lat = coordinatesField.Latitude;
            newField.lon = coordinatesField.Longitude;
            return newField;
        }
    }
}
