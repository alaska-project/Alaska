using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Query
{
    internal class ItemQueries
    {
        public Item GetItem(string id, string language, string database)
        {
            return GetDatabase(database).GetItem(id, Language.Parse(language));
        }

        public IEnumerable<Item> GetItemChildren(Item item)
        {
            return item.GetChildren();
        }

        public IEnumerable<Item> GetItemDescendants(Item item)
        {
            return item.Axes.GetDescendants();
        }

        private Database GetDatabase(string database)
        {
            return Sitecore.Configuration.Factory.GetDatabase(database) ??
                throw new ArgumentException($"Database {database} not found");
        }
    }
}
