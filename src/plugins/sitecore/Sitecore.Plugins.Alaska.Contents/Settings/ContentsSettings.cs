using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Settings
{
    internal class ContentsSettings
    {
        private const string DefaultAbsolutePathConfigKey = "Alaska.Contents.DefaultAbsolutePath";

        #region Init

        private static ContentsSettings _Instance = new ContentsSettings();
        public static ContentsSettings Current => _Instance;

        private ContentsSettings()
        { }

        #endregion

        public string DefaultAbsolutePath => GetMandatorySitecoreSetting(DefaultAbsolutePathConfigKey);

        private string GetMandatorySitecoreSetting(string key)
        {
            return Sitecore.Configuration.Settings.GetSetting(key) ??
                throw new InvalidOperationException($"Missing sitecore setting key {key}");
        }
    }
}
