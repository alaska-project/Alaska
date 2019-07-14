using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using Alaska.Extensions.Media.Azure.Infrastructure.Settings;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class AddImageThumbnailCommandHandler : IRequestHandler<AddImageThumbnailCommand, CloudBlockBlob>
    {
        private readonly AzureStorageRepository _repository;
        private readonly IImageTransformer _imageTransofrmer;
        private readonly IOptions<AzureMediaStorageOptions> _storageConfig;

        public AddImageThumbnailCommandHandler(
            AzureStorageRepository repository,
            IOptions<AzureMediaStorageOptions> storageConfig,
            IImageTransformer imageTransofrmer)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _storageConfig = storageConfig ?? throw new ArgumentNullException(nameof(storageConfig));
            _imageTransofrmer = imageTransofrmer ?? throw new ArgumentNullException(nameof(imageTransofrmer));
        }

        public async Task<CloudBlockBlob> Handle(AddImageThumbnailCommand request, CancellationToken cancellationToken)
        {
            var thumbnailContent = _imageTransofrmer.CreateImageThumbnail(request.MediaContent, _storageConfig.Value.Thumbnails.Size, _storageConfig.Value.Thumbnails.Quality);
            var folder = await _repository.GetThumbnailsDirectoryReference(request.FolderId);
            return await _repository.UploadContent(folder, request.Name, thumbnailContent, request.ContentType);
        }
    }
}
