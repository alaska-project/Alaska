using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IImageHelper
    {
        bool IsImage(string contentType, string fileName);
    }
}
