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
        public Task<MediaFolder> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
