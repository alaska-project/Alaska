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
    internal class IntFieldAdapter : IFieldAdapter
    {
        public ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Integer,
                Value = ParseValue(field.Value),
            };
        }

        private int? ParseValue(string value)
        {
            if (int.TryParse(value, out int result))
                return result;
            return null;
        }
    }
}
