using Alaska.Services.Contents.Domain.Models.Items;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Application.Commands
{
    public class UpdateItemCommand : IRequest<Unit>
    {
        public UpdateItemCommand(ContentItem contentItem)
        {
            ContentItem = contentItem;
        }

        public ContentItem ContentItem { get; }
    }
}
