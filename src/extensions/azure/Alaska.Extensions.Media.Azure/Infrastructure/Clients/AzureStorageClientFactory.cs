using Alaska.Extensions.Media.Azure.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Clients
{
    internal class AzureStorageClientFactory
    {
        private readonly IOptions<AzureMediaStorageOptions> _storageConfig;

        public AzureStorageClientFactory(IOptions<AzureMediaStorageOptions> storageConfig)
        {
            _storageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
        }

        public CloudBlobClient CreateBlobClient()
        {
            var storageCredentials = new StorageCredentials(_storageConfig.Value.StorageConnection.AccountName, _storageConfig.Value.StorageConnection.AccountKey);

            var storageAccount = new CloudStorageAccount(storageCredentials, true);

            return storageAccount.CreateCloudBlobClient();
        }
    }
}
