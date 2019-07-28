using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, MediaFolder>
    {
        private readonly IMediaLibraryService _mediaLibrary;

        public CreateFolderCommandHandler(IMediaLibraryService mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        public async Task<MediaFolder> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            return await _mediaLibrary.CreateFolder(request.FolderName, request.ParentFolderId);
        }
    }
}
