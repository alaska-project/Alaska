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
    internal class CheckboxFieldAdapter : FieldAdapter<CheckboxField>
    {
        public override ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Bool,
                Value = GetField(field).Checked,
            };
        }

        public override void UpdateField(ContentItemField value, Field field)
        {
            GetField(field).Checked = (bool)value.Value;
        }
    }
}
