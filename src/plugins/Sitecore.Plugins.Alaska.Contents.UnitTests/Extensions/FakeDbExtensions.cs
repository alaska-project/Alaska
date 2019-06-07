using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.FakeDb;
using Sitecore.Plugins.Alaska.Contents.UnitTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.UnitTests
{
    public static class FakeDbExtensions
    {
        public static Item GetItem(this ExtDbItem item)
        {
            return item.Database.GetItem(item.ID);
        }

        public static ExtDbItem WithField(this ExtDbItem item, string name, string value)
        {
            var field = new DbField(name) { Value = value };
            item.Fields.Add(field);
            return item;
        }

        public static ExtDbItem AddItem(this Db database, string name)
        {
            var item = new ExtDbItem(database, name);
            database.Add(item);
            return item;
        }
    }
}
