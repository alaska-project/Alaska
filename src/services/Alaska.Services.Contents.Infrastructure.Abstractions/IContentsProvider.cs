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
    public interface IContentsProvider
    {
        Task<ContentItem> GetPreviewItem(string itemId, string language);
        Task<ContentSearchResult> SearchContent(ContentSearchRequest contentsSearch);
        Task<ContentsSearchResult> SearchContents(ContentsSearchRequest contentsSearch);
        Task<ContentItem> CreateContent(ContentCreationRequest creationRequest);
        Task<ContentItem> UpdateContent(ContentItem contentItem);
        Task PublishContent(PublishContentRequest contentPublish);
    }
}
