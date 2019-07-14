using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IImageTransformer
    {
        byte[] CreateImageThumbnail(byte[] originalImage, int maxWidth, int maxHeight);
    }
}
