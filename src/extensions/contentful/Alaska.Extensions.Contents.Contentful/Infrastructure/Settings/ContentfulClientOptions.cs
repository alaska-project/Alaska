using System;
using System.Collections.Generic;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Infrastructure.Settings
{
    public class ContentfulClientOptions
    {
        public string DeliveryApiKey { get; set; }
        public string PreviewApiKey { get; set; }
        public string SpaceId { get; set; }
        public string ContentManagementApiToken { get; set; }
    }
}
