using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Utils
{
    internal static class FieldSerializationUtil
    {
        public static T ConvertDeserializedField<T>(object rawField)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(rawField));
        }
    }
}
