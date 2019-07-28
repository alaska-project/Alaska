using Alaska.Services.Contents.Domain.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IContentsAuthorizationMiddleware
    {
        bool CanRed(ContentItem item);
        bool CanWrite(ContentItem item);
        bool CanPublish(ContentItem item);
    }
}
