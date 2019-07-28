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

        public ContentQueries(IContentsService contentsService)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
        }

        public async Task<ContentSearchResult> GetContents(ContentsSearchRequest searchRequest)
        {
            var content = await _contentsService.SearchContent(searchRequest);
            return content;
        }
    }
}
