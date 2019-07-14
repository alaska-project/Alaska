using Alaska.Extensions.Media.Azure.Application.Converters;
using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using Alaska.Services.Contents.Domain.Models.Media;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class AddMediaCommandHandler : IRequestHandler<AddMediaCommand, MediaContent>
    {
        private readonly IMediator _mediator;
        private readonly AzureStorageRepository _repository;
        private readonly MediaContentConverter _contentConverter;
        private readonly IImageHelper _imageHelper;

        public AddMediaCommandHandler(
            IMediator mediator,
            AzureStorageRepository repository, 
            MediaContentConverter contentConverter,
            IImageHelper imageHelper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _contentConverter = contentConverter ?? throw new ArgumentNullException(nameof(contentConverter));
            _imageHelper = imageHelper ?? throw new ArgumentNullException(nameof(imageHelper));
        }

        public async Task<MediaContent> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {
            var folder = _repository.GetMediaDirectoryReference(request.FolderId);
            var content = await _repository.UploadContent(folder, request.Name, request.Content, request.ContentType);

            var thumbnail = _imageHelper.IsImage(request.ContentType, request.Name) ?
                await _mediator.Send(new AddImageThumbnailCommand(request.Name, request.ContentType, request.Content, request.FolderId)) :
                null;

            return _contentConverter.ConvertContent(content, thumbnail);
        }
    }
}
