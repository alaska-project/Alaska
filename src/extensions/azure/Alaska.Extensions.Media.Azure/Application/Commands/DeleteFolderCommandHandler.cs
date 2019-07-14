using Alaska.Extensions.Media.Azure.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Media.Azure.Application.Commands
{
    internal class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, Unit>
    {
        private readonly AzureStorageRepository _repository;

        public DeleteFolderCommandHandler(AzureStorageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            var directory = _repository.GetMediaDirectoryReference(request.FolderId);
            await _repository.DeleteDirectoryContent(directory);

            return Unit.Value;
        }
    }
}
