using Alaska.Extensions.Media.Azure.Application.Converters;
using Alaska.Extensions.Media.Azure.Infrastructure.Clients;
using Alaska.Extensions.Media.Azure.Infrastructure.Settings;
using Alaska.Services.Contents.Domain.Models.Media;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Repository
{
    internal class AzureStorageRepository
    {
        private const string PlaceholderFile = "_.txt";

        private readonly AzureStorageClientFactory _client;
        private readonly MediaFolderConverter _mediaFolderConverter;
        private readonly MediaContentConverter _mediaContentConverter;
        private readonly IOptions<AzureMediaStorageOptions> _storageConfig;

        public AzureStorageRepository(
            AzureStorageClientFactory clientFactory,
            MediaFolderConverter mediaFolderConverter,
            MediaContentConverter mediaContentConverter,
            IOptions<AzureMediaStorageOptions> storageConfig)
        {
            _client = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _mediaFolderConverter = mediaFolderConverter ?? throw new ArgumentNullException(nameof(mediaFolderConverter));
            _mediaContentConverter = mediaContentConverter ?? throw new ArgumentNullException(nameof(mediaContentConverter));
            _storageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
        }

        public async Task<CloudBlobDirectory> CreateDirectory(string name, CloudBlobContainer container)
        {
            var placeholderPath = $"{name}/{PlaceholderFile}";
            var blob = container.GetBlockBlobReference(placeholderPath);
            await blob.UploadTextAsync("_");
            return blob.Parent;
        }

        public async Task<CloudBlockBlob> UploadContent(CloudBlobDirectory folder, string name, byte[] content, string contentType)
        {
            var blob = folder.GetBlockBlobReference(name);
            await blob.UploadFromStreamAsync(new MemoryStream(content));
            blob.Properties.ContentType = contentType;
            await blob.SetPropertiesAsync();
            return blob;
        }

        public async Task<CloudBlobDirectory> CreateDirectory(string name, CloudBlobDirectory parent)
        {
            var placeholderPath = $"{name}/{PlaceholderFile}";
            var blob = parent.GetBlockBlobReference(placeholderPath);
            await blob.UploadTextAsync("_");
            return blob.Parent;
        }

        public CloudBlockBlob GetBlobReference(string id)
        {
            return RootContainerReference().GetBlockBlobReference(id);
        }

        public CloudBlobDirectory GetDirectoryReference(string id)
        {
            return RootContainerReference().GetDirectoryReference(id);
        }

        public async Task<IEnumerable<MediaFolder>> GetRootContainerDirectories()
        {
            var root = await RootContainer();
            return await GetContainerDirectories(root);
        }

        public async Task DeleteDirectoryContent(CloudBlobDirectory directory)
        {
            BlobContinuationToken continuationToken = null;

            do
            {
                var result = await directory.ListBlobsSegmentedAsync(continuationToken);

                result.Results
                    .Where(x => x is CloudBlockBlob)
                    .ToList()
                    .ForEach(async x => await ((CloudBlockBlob)x).DeleteIfExistsAsync());

                result.Results
                    .Where(x => x is CloudBlobDirectory)
                    .ToList()
                    .ForEach(async x => await DeleteDirectoryContent((CloudBlobDirectory)x));

                continuationToken = result.ContinuationToken;
            }
            while (continuationToken != null);
        }

        public async Task<IEnumerable<MediaContent>> GetChildrenBlobs(CloudBlobDirectory directory)
        {
            var blobs = new List<MediaContent>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var result = await directory.ListBlobsSegmentedAsync(continuationToken);
                blobs.AddRange(result.Results
                    .Where(x => x is CloudBlockBlob)
                    .Cast<CloudBlockBlob>()
                    .Select(x => _mediaContentConverter.ConvertContent(x))
                    .Where(x => x.Name != PlaceholderFile));
                continuationToken = result.ContinuationToken;
            }
            while (continuationToken != null);

            return blobs;
        }

        public async Task<IEnumerable<MediaFolder>> GetChildrenDirectories(CloudBlobDirectory directory)
        {
            var blobs = new List<MediaFolder>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var result = await directory.ListBlobsSegmentedAsync(continuationToken);
                blobs.AddRange(result.Results
                    .Where(x => x is CloudBlobDirectory)
                    .Select(x => _mediaFolderConverter.ConvertToMediaFolder((CloudBlobDirectory)x)));
                continuationToken = result.ContinuationToken;
            }
            while (continuationToken != null);

            return blobs;
        }

        public async Task<IEnumerable<MediaFolder>> GetContainerDirectories(CloudBlobContainer container)
        {
            var blobs = new List<MediaFolder>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var result = await container.ListBlobsSegmentedAsync(continuationToken);
                blobs.AddRange(result.Results
                    .Where(x => x is CloudBlobDirectory)
                    .Select(x => _mediaFolderConverter.ConvertToMediaFolder((CloudBlobDirectory)x)));
                continuationToken = result.ContinuationToken;
            }
            while (continuationToken != null);

            return blobs;
        }
        
        public async Task<CloudBlobContainer> GetContainer(string id)
        {
            var container = _client.CreateBlobClient().GetContainerReference(id);
            await container.CreateIfNotExistsAsync();
            return container;
        }

        public async Task<CloudBlobContainer> RootContainer()
        {
            var root = RootContainerReference();
            if (await root.ExistsAsync())
                return root;

            await root.CreateIfNotExistsAsync();
            await SetContainerPublicAccess(root, BlobContainerPublicAccessType.Blob);

            return root;
        }

        private async Task SetContainerPublicAccess(CloudBlobContainer container, BlobContainerPublicAccessType accessType)
        {
            var permissions = await container.GetPermissionsAsync();
            permissions.PublicAccess = accessType;
            await container.SetPermissionsAsync(permissions);

        }

        private CloudBlobContainer RootContainerReference()
        {
            return _client.CreateBlobClient().GetContainerReference(_storageConfig.Value.RootContainerName);
        }
    }
}
