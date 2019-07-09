using Alaska.Common.Diagnostics.Abstractions;
using Alaska.Extensions.Contents.Contentful.Caching;
using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Extensions.Contents.Contentful.Services;
using Contentful.Core.Models;
using Contentful.Core.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Query
{
    internal class ContentQueries
    {
        private readonly IProfiler _profiler;
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentTypesCache _contentTypesCache;

        public ContentQueries(
            IProfiler profiler,
            ContentfulClientsFactory factory,
            ContentTypesCache contentTypesCache)
        {
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _contentTypesCache = contentTypesCache ?? throw new ArgumentNullException(nameof(contentTypesCache));
        }

        public async Task<ContentItemData> GetContentItem(ContentItemReference item, string target)
        {
            var t = (PublishingTarget)Enum.Parse(typeof(PublishingTarget), target, true);
            return await GetContentItem(item, t);
        }

        public async Task<ContentItemData> GetContentItem(ContentItemReference item, PublishingTarget target)
        {
            return await GetContentItem(item, target == PublishingTarget.Preview);
        }

        private async Task<ContentItemData> GetContentItem(ContentItemReference item, bool preview)
        {
            return await GetContentItem<ContentItemData>(item, preview);
        }

        private async Task<T> GetContentItem<T>(ContentItemReference item, bool preview)
        {
            using (_profiler.Measure(nameof(GetContentItem)))
            {
                var query = new QueryBuilder<T>().LocaleIs(item.Locale);
                return await _factory.GetContentsClient(preview).GetEntry(item.Id, query);
            }
        }

        public ContentType GetContentType(ContentItemData entry)
        {
            var contentTypeId = (string)entry["sys"].contentType.sys.id.Value.ToString();
            return GetContentType((string)contentTypeId);
        }

        public ContentType GetContentType(string contentTypeId)
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
