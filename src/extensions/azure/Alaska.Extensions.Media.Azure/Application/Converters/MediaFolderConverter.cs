using System;
using System.Collections.Generic;
using System.Text;
using Alaska.Services.Contents.Domain.Models.Media;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Alaska.Extensions.Media.Azure.Application.Converters
{
    internal class MediaFolderConverter
    {
        public MediaFolder ConvertToMediaFolder(CloudBlobDirectory container)
        {
            return new MediaFolder
            {
                Id = container.Container.Uri.ToString(),
                Name = container.Container.Name,
            };
        }
    }
}
