using Alaska.Services.Contents.Domain.Models.Items;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ContentItem>
    {
        public Task<ContentItem> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
