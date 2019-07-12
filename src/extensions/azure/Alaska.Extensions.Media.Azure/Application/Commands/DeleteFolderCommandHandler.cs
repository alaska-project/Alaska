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
        public Task<Unit> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
