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
    internal class LinkedItemsFieldAdapter : IFieldAdapter
    {
        private readonly ItemAdapterService _itemAdapter = new ItemAdapterService();

        public ContentItemField AdaptField(Field field)
        {
            return new ContentItemField
            {
                Type = DefaultFieldTypes.ItemList,
                Value = GetLinkedItems(field).ToList(),
            };
        }

        private IEnumerable<ContentItem> GetLinkedItems(Field field)
        {
            var items = GetLinkedSitecoreItems(field);

            return items?
                .Select(x => _itemAdapter.AdaptItem(x))
                .ToList();
        }

        private IEnumerable<Item> GetLinkedSitecoreItems(Field field)
        {
            var idList = field.Value?
                .Split('|')
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            return idList?
                .Select(x => field.Item.Database.GetItem(x, field.Item.Language))
                .ToList();
        }
    }
}
