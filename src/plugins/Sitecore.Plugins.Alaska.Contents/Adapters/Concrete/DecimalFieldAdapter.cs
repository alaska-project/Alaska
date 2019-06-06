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
    internal class DecimalFieldAdapter : IFieldAdapter
    {
        public ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Decimal,
                Value = ParseValue(field.Value),
            };
        }

        public void UpdateField(ContentItemField value, Field field)
        {
            throw new NotImplementedException();
        }

        private decimal? ParseValue(string value)
        {
            if (decimal.TryParse(value, out decimal result))
                return result;
            return null;
        }
    }
}
