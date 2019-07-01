using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IContentsService
    {
        Task<ContentSearchResult> SearchContent(ContentsSearchRequest contentsSearch);
        Task<ContentItem> UpdateContent(ContentItem contentItem);
        Task<ContentItem> PublishContent(PublishContentRequest contentPublish);
    }
}
