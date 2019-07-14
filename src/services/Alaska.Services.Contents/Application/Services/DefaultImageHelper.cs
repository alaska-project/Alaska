using Alaska.Services.Contents.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alaska.Services.Contents.Application.Services
{
    internal class DefaultImageHelper : IImageHelper
    {
        public bool IsImage(string contentType, string fileName)
        {
            if (contentType.StartsWith("image/", StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }
    }
}
