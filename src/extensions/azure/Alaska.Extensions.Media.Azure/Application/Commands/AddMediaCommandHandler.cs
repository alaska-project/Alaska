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
        public Task<MediaContent> Handle(AddMediaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
