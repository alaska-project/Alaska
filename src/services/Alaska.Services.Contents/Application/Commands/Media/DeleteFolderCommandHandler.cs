using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Unit>
    {
        private readonly IMediaLibraryService _mediaLibrary;

        public DeleteFolderCommandHandler(IMediaLibraryService mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            await _mediaLibrary.DeleteFolder(request.FolderId);

            return Unit.Value;
        }
    }
}
