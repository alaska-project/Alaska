using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands
{
    internal class CreateContentCommand : IRequest<ContentItem>
    {
        public CreateContentCommand(ContentCreationRequest request)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public ContentCreationRequest Request { get; }
    }
}
