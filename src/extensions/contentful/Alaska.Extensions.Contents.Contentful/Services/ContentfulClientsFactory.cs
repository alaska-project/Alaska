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

    internal class ContentfulClientsFactory
    {
        private readonly IOptions<ContentfulClientOptions> _options;

        public ContentfulClientsFactory(IOptions<ContentfulClientOptions> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IContentfulClient GetContentsClient()
        {
            var httpClient = new HttpClient();
            return new ContentfulClient(httpClient, ContentfulOptions());
        }

        public IContentfulManagementClient GetContentManagementClient()
        {
            var httpClient = new HttpClient();
            return new ContentfulManagementClient(httpClient, _options.Value.ContentManagementApiToken, _options.Value.SpaceId);
        }

        private ContentfulOptions ContentfulOptions()
        {
            return new ContentfulOptions()
            {
                DeliveryApiKey = _options.Value.DeliveryApiKey,
                PreviewApiKey = _options.Value.PreviewApiKey,
                SpaceId = _options.Value.SpaceId,
            };
        }
    }
}
