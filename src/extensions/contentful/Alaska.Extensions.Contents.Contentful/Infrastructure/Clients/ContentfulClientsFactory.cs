using Alaska.Extensions.Contents.Contentful.Infrastructure.Settings;
using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Alaska.Extensions.Contents.Contentful.Infrastructure.Clients
{
    public enum ContentfulPublishingTarget { Staging, Prod }

    internal class ContentfulClientsFactory
    {
        private readonly IOptions<ContentfulClientOptions> _options;

        public ContentfulClientsFactory(IOptions<ContentfulClientOptions> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IContentfulClient GetContentsClient() => GetContentsClient(false);

        public IContentfulClient GetContentsClient(bool preview)
        {
            var httpClient = new HttpClient();
            return new ContentfulClient(httpClient, ContentfulOptions(preview));
        }

        public IContentfulManagementClient GetContentManagementClient()
        {
            var httpClient = new HttpClient();
            return new ContentfulManagementClient(httpClient, _options.Value.ContentManagementApiToken, _options.Value.SpaceId);
        }

        private ContentfulOptions ContentfulOptions(bool preview)
        {
            return new ContentfulOptions()
            {
                DeliveryApiKey = _options.Value.DeliveryApiKey,
                PreviewApiKey = _options.Value.PreviewApiKey,
                SpaceId = _options.Value.SpaceId,
                UsePreviewApi = preview,
            };
        }
    }
}
