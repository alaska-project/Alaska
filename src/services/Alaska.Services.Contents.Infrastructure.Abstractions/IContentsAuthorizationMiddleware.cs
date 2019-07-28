using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IContentsAuthorizationMiddleware
    {
        bool CanRead(ContentItem item);
        bool CanCreate(ContentCreationRequest request);
        bool CanWrite(ContentItem item);
        bool CanPublish(ContentItem item);
    }
}
