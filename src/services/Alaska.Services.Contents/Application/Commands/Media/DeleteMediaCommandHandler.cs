using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Commands.Media
{
    internal class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Unit>
    {
        private readonly IMediaLibraryService _mediaLibrary;

        public DeleteMediaCommandHandler(IMediaLibraryService mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        public async Task<Unit> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            await _mediaLibrary.DeleteMedia(request.MediaId);

            return Unit.Value;
        }
    }
}
