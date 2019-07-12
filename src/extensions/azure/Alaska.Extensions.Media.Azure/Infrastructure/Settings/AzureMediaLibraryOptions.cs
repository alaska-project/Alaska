using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Settings
{
    public class AzureMediaStorageOptions
    {
        public AzureStorageConnectionSettings StorageConnection { get; set; }
        public string RootContainer { get; set; }
    }
}
