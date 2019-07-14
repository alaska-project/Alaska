using Alaska.Extensions.Media.Azure.Application.Converters;
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
    internal class CreateRootFolderCommandHandler : IRequestHandler<CreateRootFolderCommand, MediaFolder>
    {
        private readonly AzureStorageRepository _repository;
        private readonly MediaFolderConverter _folderConverter;

        public CreateRootFolderCommandHandler(AzureStorageRepository repository, MediaFolderConverter folderConverter)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _folderConverter = folderConverter ?? throw new ArgumentNullException(nameof(folderConverter));
        }

        public async Task<MediaFolder> Handle(CreateRootFolderCommand request, CancellationToken cancellationToken)
        {
            var rootContainer = await _repository.MediaContainer();
            var newContainer = await _repository.CreateDirectory(request.FolderName, rootContainer);
            return _folderConverter.ConvertToMediaFolder(newContainer);
        }
    }
}
