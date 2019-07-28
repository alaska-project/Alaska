using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Queries
{
    internal class ContentQueries : IContentQueries
    {
        private readonly IContentsService _contentsService;
        private readonly IContentsAuthorizationMiddleware _auth;

        public ContentQueries(
            IContentsService contentsService,
            IContentsAuthorizationMiddleware auth = null)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
            _auth = auth;
        }

        public async Task<ContentSearchResult> GetContents(ContentsSearchRequest searchRequest)
        {
            var content = await _contentsService.SearchContent(searchRequest);

            if (content.Item == null || !CanRead(content.Item.Value))
                return null;

            return content;
        }

        private bool CanRead(ContentItem item)
        {
            return _auth == null || _auth.CanRead(item);
        }
    }
}
