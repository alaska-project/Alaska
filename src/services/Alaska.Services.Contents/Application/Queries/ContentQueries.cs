using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Queries
{
    internal class ContentQueries : IContentQueries
    {
        private readonly IContentsProvider _contentsService;
        private readonly IContentsAuthorizationMiddleware _auth;

        public ContentQueries(
            IContentsProvider contentsService,
            IContentsAuthorizationMiddleware auth = null)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
            _auth = auth;
        }

        public async Task<ContentSearchResult> GetContent(ContentSearchRequest searchRequest)
        {
            var result = await _contentsService.SearchContent(searchRequest);

            return FilterSearchResult(result);
        }

        public async Task<ContentsSearchResult> SearchContents(ContentsSearchRequest searchRequest)
        {
            var result = await _contentsService.SearchContents(searchRequest);

            return FilterSearchResults(result);
        }

        private ContentsSearchResult FilterSearchResults(ContentsSearchResult searchResults)
        {
            return new ContentsSearchResult
            {
                Items = searchResults.Items
                    .Where(x => CanRead(x))
                    .ToList(),
            };
        }

        private ContentSearchResult FilterSearchResult(ContentSearchResult searchResult)
        {
            if (searchResult.Item == null || !CanRead(searchResult.Item.Value))
                return null;

            return searchResult;
        }

        private bool CanRead(ContentItem item)
        {
            return _auth == null || _auth.CanRead(item);
        }
    }
}
