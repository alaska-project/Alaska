using Alaska.Services.Contents.Domain.Exceptions;
using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Contentful.Core.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Services
{
    internal class ContentsService : IContentsService
    {
        private readonly ContentsClientFactory _factory;
        private readonly ContentsConverter _converter;

        public ContentsService(ContentsClientFactory factory, ContentsConverter converter)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public async Task<ContentSearchResult> SearchContent(ContentsSearchRequest contentsSearch)
        {
            if (contentsSearch.GetDepth() != ContentsSearchDepth.Item)
                throw new UnsupportedFeatureException($"{contentsSearch.GetDepth()}  not supported by Contentful provider");

            var query = new QueryBuilder<object>().LocaleIs(contentsSearch.Language);
            var entry = await _factory.GetClient().GetEntry<dynamic>(contentsSearch.Id, query);
            return new ContentSearchResult
            {
                Item = new ContentItemResult
                {
                    Value = _converter.ConvertToContentItem(entry),
                },
            };
        }

        public Task<ContentItem> UpdateContent(ContentItem contentItem)
        {
            throw new NotImplementedException();
        }

        public Task<ContentItem> PublishContent(PublishContentRequest contentPublish)
        {
            throw new NotImplementedException();
        }
    }
}
 