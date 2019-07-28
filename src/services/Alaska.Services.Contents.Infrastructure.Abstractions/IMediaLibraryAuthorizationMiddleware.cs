using Alaska.Services.Contents.Domain.Models.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IMediaLibraryAuthorizationMiddleware
    {
        bool CanRed(MediaFolder folder);
        bool CanWrite(MediaFolder folder);
    }
}
