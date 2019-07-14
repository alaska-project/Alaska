using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alaska.Services.Contents.Domain.Models.Media;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Alaska.Extensions.Media.Azure.Application.Converters
{
    internal class MediaContentConverter
    {
        public MediaContent ConvertContent(CloudBlockBlob content, CloudBlockBlob thumbnail)
        {
            return new MediaContent
            {
                Id = content.Name,
                Name = content.Name.TrimEnd('/').Split('/').Last(),
                Url = content.Uri.ToString(),
                ThumbnailUrl = thumbnail?.Uri.ToString(),
                ContentType = content.Properties.ContentType,
            };
        }
    }
}
