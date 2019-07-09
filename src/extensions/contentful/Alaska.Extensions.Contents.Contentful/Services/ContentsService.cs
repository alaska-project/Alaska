using Alaska.Common.Diagnostics.Abstractions;
using Alaska.Extensions.Contents.Contentful.Caching;
using Alaska.Extensions.Contents.Contentful.Extensions;
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
        private readonly IProfiler _profiler;
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentTypesCache _contentTypesCache;
        private readonly ContentsConverter _converter;

        public ContentsService(
            IProfiler profiler,
            ContentfulClientsFactory factory,
            ContentTypesCache contentTypesCache,
            ContentsConverter converter)
        {
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _contentTypesCache = contentTypesCache ?? throw new ArgumentNullException(nameof(contentTypesCache));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public async Task<ContentSearchResult> SearchContent(ContentsSearchRequest contentsSearch)
        {
            if (contentsSearch.GetDepth() != ContentsSearchDepth.Item)
                throw new UnsupportedFeatureException($"{contentsSearch.GetDepth()}  not supported by Contentful provider");

            var entry = await GetContentItem(new ContentItemReference
            {
                Id = contentsSearch.Id,
                Locale = contentsSearch.Language,
            }, contentsSearch.PublishingTarget);

            var contentType = GetContentType(entry);

            return ConvertToContentSearchResult(entry, contentType, contentsSearch.PublishingTarget);
        }

        public async Task<ContentItem> UpdateContent(ContentItem contentItem)
        {
            var contentManagementClient = _factory.GetContentManagementClient();

            var contentType = GetContentType(contentItem.Info.TemplateId);

            var entry = _converter.ConvertToContentEntry(contentItem, contentType);

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
            var updatedItem = await GetContentItem(contentItem.GetReference(), contentItem.Info.PublishingTarget);
            var contentType = GetContentType(contentItem.Info.TemplateId);
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

        private async Task<ContentItemData> GetContentItem(ContentItemReference item, string target)
        {
            var t = (PublishingTarget)Enum.Parse(typeof(PublishingTarget), target, true);
            return await GetContentItem(item, t);
        }

        private async Task<ContentItemData> GetContentItem(ContentItemReference item, PublishingTarget target)
        {
            return await GetContentItem(item, target == PublishingTarget.Preview);
        }

        private async Task<ContentItemData> GetContentItem(ContentItemReference item, bool preview)
        {
            using (_profiler.Measure(nameof(GetContentItem)))
            {
                var query = new QueryBuilder<ContentItemData>().LocaleIs(item.Locale);
                return await _factory.GetContentsClient(preview).GetEntry(item.Id, query);
            }
        }
        
        private ContentType GetContentType(ContentItemData entry)
        {
            var contentTypeId = (string)entry["sys"].contentType.sys.id.Value.ToString();
            return GetContentType((string)contentTypeId);
        }

        private ContentType GetContentType(string contentTypeId)
        {
            return _contentTypesCache.RetreiveContentType(contentTypeId, () => GetContentTypeSync(contentTypeId));
        }

        private ContentType GetContentTypeSync(string contentTypeId)
        {
            var t = ReadContentType(contentTypeId);
            t.Wait();
            return t.Result;
        }

        private async Task<ContentType> ReadContentType(string contentTypeId)
        {
            using (_profiler.Measure(nameof(GetContentType)))
            {
                return await _factory.GetContentManagementClient().GetContentType(contentTypeId);
            }
        }
    }
}
