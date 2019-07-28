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
    internal class AddMediaCommandHandler : IRequestHandler<AddMediaCommand, MediaContent>
    {
        private readonly IMediaLibraryService _mediaLibrary;

        public AddMediaCommandHandler(IMediaLibraryService mediaLibrary)
        {
            _mediaLibrary = mediaLibrary ?? throw new ArgumentNullException(nameof(mediaLibrary));
        }

        public async Task<MediaContent> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {
            return await _mediaLibrary.AddMedia(request.MediaContent.Name, request.MediaContent.ContentType, Convert.FromBase64String(request.MediaContent.MediaContent), request.MediaContent.FolderId);
        }
    }
}
