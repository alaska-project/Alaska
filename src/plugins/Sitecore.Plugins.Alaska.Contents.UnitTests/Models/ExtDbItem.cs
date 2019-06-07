using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.UnitTests.Models
{
    public class ExtDbItem : DbItem
    {
        private Db _database;

        public ExtDbItem(Db database, string name)
            : base(name, new Data.ID(Guid.NewGuid()))
        {
            _database = database;
        }

        public Db Database => _database;
    }
}
