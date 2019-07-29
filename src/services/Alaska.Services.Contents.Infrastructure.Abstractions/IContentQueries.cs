using Alaska.Services.Contents.Domain.Models.Search;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Infrastructure.Abstractions
{
    public interface IContentQueries
    {
        Task<ContentSearchResult> GetContent(ContentSearchRequest searchRequest);
        Task<ContentsSearchResult> SearchContents(ContentsSearchRequest searchRequest);
    }
}
