using Alaska.Common.Diagnostics.Abstractions;
using Alaska.Extensions.Contents.Contentful.Application.Extensions;
using Alaska.Extensions.Contents.Contentful.Application.Query;
using Alaska.Extensions.Contents.Contentful.Converters;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Exceptions;
using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Contentful.Core.Models;
using Contentful.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Services
{
    internal class ContentsService : IContentsService
    {
        private readonly ContentQueries _query;
        private readonly IProfiler _profiler;
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentsConverter _converter;

        public ContentsService(
            ContentQueries query,
            IProfiler profiler,
            ContentfulClientsFactory factory,
            ContentsConverter converter)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public async Task<ContentSearchResult> SearchContent(ContentsSearchRequest contentsSearch)
        {
            if (contentsSearch.GetDepth() != ContentsSearchDepth.Item)
                throw new UnsupportedFeatureException($"{contentsSearch.GetDepth()}  not supported by Contentful provider");

            var entry = await _query.GetContentItem(new ContentItemReference
            {
                Id = contentsSearch.Id,
                Locale = contentsSearch.Language,
            }, contentsSearch.PublishingTarget);

            var contentType = _query.GetContentType(entry);

            return ConvertToContentSearchResult(entry, contentType, contentsSearch.PublishingTarget);
        }

        public async Task<ContentItem> UpdateContent(ContentItem contentItem)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var contentType = _query.GetContentType(contentItem.Info.TemplateId);

            var entry = _query.GetContentItem(contentItem.GetReference(), PublishingTarget.Preview);
            //var entry = _converter.ConvertToContentEntry(contentItem, contentType);

            await contentManagementClient.UpdateEntryForLocale(entry, contentItem.Info.Id, contentItem.Info.Language);

            return await ReloadContentItem(contentItem);
        }

        public async Task PublishContent(PublishContentRequest contentPublish)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var entry = await contentManagementClient.GetEntry(contentPublish.ItemId);

            await contentManagementClient.PublishEntry(entry.SystemProperties.Id, entry.SystemProperties.Version.Value);
        }

        private async Task<ContentItem> ReloadContentItem(ContentItem contentItem)
        {
            var updatedItem = await _query.GetContentItem(contentItem.GetReference(), contentItem.Info.PublishingTarget);
            var contentType = _query.GetContentType(contentItem.Info.TemplateId);
            return _converter.ConvertToContentItem(updatedItem, contentType, contentItem.Info.PublishingTarget);
        }

        private ContentSearchResult ConvertToContentSearchResult(ContentItemData entry, ContentType contentType, string target)
        {
            using (_profiler.Measure(nameof(ConvertToContentSearchResult)))
            {
                return new ContentSearchResult
                {
                    Item = new ContentItemResult
                    {
                        Value = _converter.ConvertToContentItem(entry, contentType, target),
                    },
                };
            }
        }
    }
}
