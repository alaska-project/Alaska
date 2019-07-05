using Alaska.Common.Diagnostics.Abstractions;
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
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Services
{
    internal class ContentsService : IContentsService
    {
        private readonly IProfiler _profiler;
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentsConverter _converter;

        public ContentsService(
            IProfiler profiler,
            ContentfulClientsFactory factory, 
            ContentsConverter converter)
        {
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public async Task<ContentSearchResult> SearchContent(ContentsSearchRequest contentsSearch)
        {
            if (contentsSearch.GetDepth() != ContentsSearchDepth.Item)
                throw new UnsupportedFeatureException($"{contentsSearch.GetDepth()}  not supported by Contentful provider");

            var entry = await GetContentItem(contentsSearch);

            var contentType = await GetContentType(entry);

            return ConvertToContentSearchResult(entry, contentType);
        }

        public Task<ContentItem> UpdateContent(ContentItem contentItem)
        {
            throw new NotImplementedException();
        }

        public Task<ContentItem> PublishContent(PublishContentRequest contentPublish)
        {
            throw new NotImplementedException();
        }

        private ContentSearchResult ConvertToContentSearchResult(ContentItemData entry, ContentType contentType)
        {
            using (_profiler.Measure(nameof(ConvertToContentSearchResult)))
            {
                return new ContentSearchResult
                {
                    Item = new ContentItemResult
                    {
                        Value = _converter.ConvertToContentItem(entry, contentType),
                    },
                };
            }
        }

        private async Task<ContentItemData> GetContentItem(ContentsSearchRequest contentsSearch)
        {
            using (_profiler.Measure(nameof(GetContentItem)))
            {
                var query = new QueryBuilder<ContentItemData>().LocaleIs(contentsSearch.Language);
                return await _factory.GetContentsClient().GetEntry(contentsSearch.Id, query);
            }
        }

        private async Task<ContentType> GetContentType(ContentItemData entry)
        {
            using (_profiler.Measure(nameof(GetContentType)))
            {
                var contentTypeId = entry["sys"].contentType.sys.id.Value.ToString();
                return await _factory.GetContentManagementClient().GetContentType(contentTypeId);
            }
        }
    }
}
 