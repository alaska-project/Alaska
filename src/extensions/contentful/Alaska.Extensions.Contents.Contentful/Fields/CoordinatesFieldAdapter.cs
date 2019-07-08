﻿using Alaska.Extensions.Contents.Contentful.Abstractions;
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

        public dynamic WriteField(ContentItemField field, Field fieldDefinition)
        {
            return new
            {
                lat = ((CoordinatesField)field.Value).Latitude,
                lon = ((CoordinatesField)field.Value).Longitude,
            };
        }
    }
}
