using Alaska.Extensions.Contents.Contentful.Settings;
using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Services
{
    public enum ContentfulPublishingTarget { Staging, Prod }

    internal class ContentsClientFactory
    {
        private readonly IOptions<ContentfulClientOptions> _options;

        public ContentsClientFactory(IOptions<ContentfulClientOptions> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IContentfulClient GetClient()
        {
            var httpClient = new HttpClient();
            var options = new ContentfulOptions()
            {
                DeliveryApiKey = _options.Value.DeliveryApiKey,
                PreviewApiKey = _options.Value.PreviewApiKey,
                SpaceId = _options.Value.SpaceId,
            };
            
            return new ContentfulClient(httpClient, options);
        }
    }
}
