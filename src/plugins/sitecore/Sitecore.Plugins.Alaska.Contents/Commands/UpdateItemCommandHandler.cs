using Alaska.Services.Contents.Domain.Models.Items;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Plugins.Alaska.Contents.Query;
using Sitecore.Plugins.Alaska.Contents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Commands
{
    internal class UpdateItemCommandHandler
    {
        private ItemQueries _query = new ItemQueries();
        private FieldAdapterService _fieldAdapter = new FieldAdapterService();
        private ItemAdapterService _itemAdapter = new ItemAdapterService();

        public ContentItem Handle(UpdateItemCommand command)
        {
            var item = GetSitecoreItem(command.Item);
            using (new EditContext(item))
            {
                command.Item.Fields
                    .ToList()
                    .ForEach(x => UpdateField(x.Key, x.Value, item));
                ItemManager.SaveItem(item);
            }

            return _itemAdapter.AdaptItem(item).Value;
        }

        private void UpdateField(string fieldName, ContentItemField fieldValue, Item item)
        {
            var sitecoreField = GetField(fieldName, item);
            _fieldAdapter.UpdateField(fieldValue, sitecoreField);
        }

        private Field GetField(string fieldName, Item item)
        {
            return item.Fields[fieldName];
        }

        private Item GetSitecoreItem(ContentItem item)
        {
            return _query.GetItem(item.Info.Id, item.Info.Language, item.Info.PublishingTarget);
        }
    }
}
