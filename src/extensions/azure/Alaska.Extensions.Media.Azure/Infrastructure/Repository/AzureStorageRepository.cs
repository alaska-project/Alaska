using Alaska.Extensions.Media.Azure.Application.Converters;
using Alaska.Extensions.Media.Azure.Infrastructure.Clients;
using Alaska.Services.Contents.Domain.Models.Media;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Repository
{
    internal class AzureStorageRepository
    {
        private readonly AzureStorageClientFactory _client;
        private readonly MediaFolderConverter _mediaFolderConverter;
        private readonly MediaContentConverter _mediaContentConverter;

        public AzureStorageRepository(
            AzureStorageClientFactory clientFactory,
            MediaFolderConverter mediaFolderConverter,
            MediaContentConverter mediaContentConverter)
        {
            _client = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _mediaFolderConverter = mediaFolderConverter ?? throw new ArgumentNullException(nameof(mediaFolderConverter));
            _mediaContentConverter = mediaContentConverter ?? throw new ArgumentNullException(nameof(mediaContentConverter));
        }

        public async CloudBlobContainer CreateContainer(string name, CloudBlobContainer parent)
        {
            var containerReference = _client.CreateBlobClient().GetContainerReference($"{parent.Name}/{name}");
            await containerReference.CreateAsync();
            return containerReference;
        }

        public async Task<IEnumerable<MediaFolder>> GetRootContainerDirectories()
        {
            return await GetContainerDirectories(RootContainer);
        }

        public async Task<IEnumerable<MediaFolder>> GetContainerDirectories(CloudBlobContainer container)
        {
            var blobs = new List<MediaFolder>();
            BlobContinuationToken continuationToken = null;

            do
            {
                var result = await RootContainer.ListBlobsSegmentedAsync(continuationToken);
                blobs.AddRange(result.Results
                    .Where(x => x is CloudBlobDirectory)
                    .Select(x => _mediaFolderConverter.ConvertToMediaFolder((CloudBlobDirectory)x)));
                continuationToken = result.ContinuationToken;
            }
            while (continuationToken != null);

            return blobs;
        }

        public CloudBlobContainer GetContainer(string id)
        {
            return _client.CreateBlobClient().GetContainerReference(id);
        }

        public CloudBlobContainer RootContainer => _client.CreateBlobClient().GetRootContainerReference();
    }
}
