using Alaska.Extensions.Media.Azure.Application.Converters;
using Alaska.Extensions.Media.Azure.Infrastructure.Clients;
using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using Alaska.Services.Contents.Domain.Models.Media;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, MediaFolder>
    {
        private readonly AzureStorageRepository _repository;
        private readonly MediaFolderConverter _folderConverter;

        public CreateFolderCommandHandler(AzureStorageRepository repository, MediaFolderConverter folderConverter)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _folderConverter = folderConverter ?? throw new ArgumentNullException(nameof(folderConverter));
        }

        public async Task<MediaFolder> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var parentDirectory = _repository.GetMediaDirectoryReference(request.ParentFolderId);
            var newDirectory = await _repository.CreateDirectory(request.FolderName, parentDirectory);
            return _folderConverter.ConvertToMediaFolder(newDirectory);
        }
    }
}
