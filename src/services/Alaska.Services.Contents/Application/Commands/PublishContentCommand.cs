using Alaska.Services.Contents.Domain.Models.Publishing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands
{
    internal class PublishContentCommand : IRequest<Unit>
    {
        public PublishContentCommand(PublishContentRequest publishingRequest)
        {
            PublishingRequest = publishingRequest ?? throw new ArgumentNullException(nameof(publishingRequest));
        }

        public PublishContentRequest PublishingRequest { get; }
    }
}
