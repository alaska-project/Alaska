using Alaska.Services.Contents.Domain.Models.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sitecore.Plugins.Alaska.Contents.Adapters.Concrete;
using Sitecore.Plugins.Alaska.Contents.UnitTests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.UnitTests.Adapters
{
    [TestClass]
    public class DefaultFieldAdapterTests
    {
        private DefaultFieldAdapter _adapter = new DefaultFieldAdapter();
        private Db _database = DbBuilder.Master;

        [TestMethod]
        public void ReadStringField()
        {
            var item = _database
                .AddItem("item")
                .WithField("field1", "value")
                .GetItem();

            var adaptedField = _adapter.AdaptField(item.Fields["field1"]);
            Assert.AreEqual("value", adaptedField.Value);
            Assert.AreEqual("string", adaptedField.Type);
        }

        [TestMethod]
        public void WriteStringField()
        {
            var item = _database
                .AddItem("item")
                .WithField("field1", "")
                .GetItem();

            _adapter.UpdateField(new ContentItemField
            {
                Type = "string",
                Value = "value",
            }, item.Fields["field1"]);

            var reloadedItem = item.Database.GetItem(item.ID);

            Assert.AreEqual("value", reloadedItem.Fields["field1"].Value);
        }
    }
}
