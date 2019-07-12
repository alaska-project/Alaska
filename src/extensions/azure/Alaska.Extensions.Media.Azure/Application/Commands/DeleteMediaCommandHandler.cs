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
        public Task<Unit> Handle(DeleteMediaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
