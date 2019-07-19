using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class DeleteMediaCommandHandler : IRequestHandler<DeleteMediaCommand, Unit>
    {
        private readonly AzureStorageRepository _repository;

        public DeleteMediaCommandHandler(AzureStorageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            var blob = _repository.GetMediaBlobReference(request.MediaId);

            var thumb = await _repository.GetThumbnailIfExists(blob);

            await blob.DeleteIfExistsAsync();

            if (thumb != null)
                await thumb.DeleteIfExistsAsync();

            return Unit.Value;
        }
    }
}
