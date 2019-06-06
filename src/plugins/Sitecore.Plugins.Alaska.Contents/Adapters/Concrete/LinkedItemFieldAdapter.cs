using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Plugins.Alaska.Contents.Abstractions;
using Sitecore.Plugins.Alaska.Contents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Adapters.Concrete
{
    internal class LinkedItemFieldAdapter : IFieldAdapter
    {
        private readonly ItemAdapterService _itemAdapter = new ItemAdapterService();

        public ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.Item,
                Value = GetLinkedItem(field),
            };
        }

        public void UpdateField(ContentItemField value, Field field)
        {
            throw new NotImplementedException();
        }

        private ContentItemResult GetLinkedItem(Field field)
        {
            var item = GetLinkedSitecoreItem(field);
            return item == null ? 
                null : 
                _itemAdapter.AdaptItem(item);
        }

        private Item GetLinkedSitecoreItem(Field field)
        {
            if (string.IsNullOrEmpty(field.Value))
                return null;

            return field.Item.Database.GetItem(field.Value, field.Item.Language);
        }
    }
}
