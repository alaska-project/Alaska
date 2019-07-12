using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alaska.Services.Contents.Domain.Models.Media;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Alaska.Extensions.Media.Azure.Application.Converters
{
    internal class MediaFolderConverter
    {
        public MediaFolder ConvertToMediaFolder(CloudBlobDirectory directory)
        {
            return new MediaFolder
            {
                Id = directory.Prefix,
                Name = directory.Prefix.TrimEnd('/').Split('/').Last(),
            };
        }
    }
}
