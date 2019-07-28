using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    internal class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, Unit>
    {
        public Task<Unit> Handle(CreateContentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
