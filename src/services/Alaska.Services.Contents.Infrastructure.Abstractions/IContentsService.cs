using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Domain.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IContentsService
    {
        Task<ContentSearchResult> GetContent(ContentSearchRequest searchRequest);
        Task<ContentsSearchResult> SearchContents(ContentsSearchRequest searchRequest);
        Task<ContentItem> CreateContent(ContentCreationRequest request);
        Task<ContentItem> UpdateContent(ContentItem item);
        Task PublishContent(PublishContentRequest publishingRequest);

    }
}
