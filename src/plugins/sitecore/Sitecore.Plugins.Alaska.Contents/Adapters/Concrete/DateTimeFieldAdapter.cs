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
    internal class DateTimeFieldAdapter : FieldAdapter<DateField>
    {
        public override ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.DateTime,
                Value = GetDateTime(field),
            };
        }

        public override void UpdateField(ContentItemField value, Field field)
        {
            var dateTime = (DateTime?)value.Value;
            field.Value = dateTime.HasValue ?
                DateUtil.ToIsoDate(dateTime.Value) :
                string.Empty;
        }

        private DateTime? GetDateTime(Field field)
        {
            if (string.IsNullOrEmpty(field.Value))
                return null;

            return GetField(field).DateTime;
        }

        public override DateField GetField(Field field)
        {
            return (DateField)field;
        }
    }
}
