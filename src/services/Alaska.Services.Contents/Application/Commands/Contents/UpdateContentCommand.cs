using Alaska.Services.Contents.Domain.Models.Items;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Application.Commands
{
    internal class UpdateContentCommand : IRequest<ContentItem>
    {
        public UpdateContentCommand(ContentItem item)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public ContentItem Item { get; }
    }
}
