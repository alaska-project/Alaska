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
    public class HtmlFieldAdapter : IFieldAdapter
    {
        public ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Html,
                Value = field.Value,
            };
        }

        public void UpdateField(ContentItemField value, Field field)
        {
            field.Value = (string)value.Value;
        }
    }
}
