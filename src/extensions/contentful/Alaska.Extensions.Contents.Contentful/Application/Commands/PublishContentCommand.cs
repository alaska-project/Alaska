using Alaska.Services.Contents.Domain.Models.Publishing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    public class PublishContentCommand : IRequest<Unit>
    {
        public PublishContentCommand(PublishContentRequest contentPublishRequest)
        {
            ContentPublishRequest = contentPublishRequest;
        }

        public PublishContentRequest ContentPublishRequest { get; }
    }
}
