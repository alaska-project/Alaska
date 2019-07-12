using Alaska.Extensions.Media.Azure.Application.Converters;
using Alaska.Extensions.Media.Azure.Infrastructure.Clients;
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
    internal class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, MediaFolder>
    {
        private readonly AzureStorageRepository _repository;

        public CreateFolderCommandHandler(AzureStorageRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<MediaFolder> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var container = _repository.GetContainer(request.Parent.Id);
            if (container == null)
                throw new InvalidOperationException($"Container {request.Parent.Id} not found");

            
        }
    }
}
