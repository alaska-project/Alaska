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
    internal class CreateRootFolderCommandHandler : IRequestHandler<CreateRootFolderCommand, MediaFolder>
    {
        private readonly IMediaLibraryService _mediaLibrary;

        public CreateRootFolderCommandHandler(IMediaLibraryService mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        public async Task<MediaFolder> Handle(CreateRootFolderCommand request, CancellationToken cancellationToken)
        {
            return await _mediaLibrary.CreateRootFolder(request.FolderName);
        }
    }
}
