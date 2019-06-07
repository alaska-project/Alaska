using Sitecore.Data;
using Sitecore.FakeDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.UnitTests.Utils
{
    public static class DbBuilder
    {
        public static Db Master => Database("master");
        public static Db Web => Database("web");

        private static Db Database(string name) => new Db(name);
    }
}
