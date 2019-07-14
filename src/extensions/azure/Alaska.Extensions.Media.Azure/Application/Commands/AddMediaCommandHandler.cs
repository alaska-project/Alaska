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
    internal class AddMediaCommandHandler : IRequestHandler<AddMediaCommand, MediaContent>
    {
        private readonly AzureStorageRepository _repository;
        private readonly MediaContentConverter _contentConverter;

        public AddMediaCommandHandler(AzureStorageRepository repository, MediaContentConverter contentConverter)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _contentConverter = contentConverter ?? throw new ArgumentNullException(nameof(contentConverter));
        }

        public async Task<MediaContent> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {
            var folder = _repository.GetMediaDirectoryReference(request.FolderId);
            var content = await _repository.UploadContent(folder, request.Name, request.Content, request.ContentType);
            return _contentConverter.ConvertContent(content);
        }
    }
}
