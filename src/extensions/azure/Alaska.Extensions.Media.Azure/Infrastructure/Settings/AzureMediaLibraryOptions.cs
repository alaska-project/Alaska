using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Settings
{
    public class AzureMediaStorageOptions
    {
        public AzureStorageConnectionSettings StorageConnection { get; set; }
        public MediaContainersSettings Containers { get; set; } = new MediaContainersSettings();
        public ImageThumbnailsSettings Thumbnails { get; set; } = new ImageThumbnailsSettings();
    }
}
