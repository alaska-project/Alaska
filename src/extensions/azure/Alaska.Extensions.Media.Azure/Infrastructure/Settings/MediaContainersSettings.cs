using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Media.Azure.Infrastructure.Settings
{
    public class MediaContainersSettings
    {
        public string MainContainerName { get; set; } = "media";

        public string ThumbnailsContainerName { get; set; } = "thumb";
    }
}
