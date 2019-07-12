using Alaska.Extensions.Media.Azure.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IOptions<AzureMediaStorageOptions> _storageConfig;

        public AzureStorageClientFactory(
            IConfiguration configuration,
            IOptions<AzureMediaStorageOptions> storageConfig)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _storageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
        }

        public CloudBlobClient CreateBlobClient()
        {
            var accountKey = _configuration.GetConnectionString(_storageConfig.Value.StorageConnection.AccountKeyConnectionStringName);

            var storageCredentials = new StorageCredentials(_storageConfig.Value.StorageConnection.AccountName, accountKey);

            var storageAccount = new CloudStorageAccount(storageCredentials, true);

            return storageAccount.CreateCloudBlobClient();
        }
    }
}
