using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Search;
using Sitecore.Plugins.Alaska.Contents.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Plugins.Alaska.Contents.Services
{
    internal class ItemSearchService
    {
        private readonly ItemAdapterService _adapter = new ItemAdapterService();
        private readonly ItemQueries _query = new ItemQueries();

        public ContentSearchResult Search(ContentSearchRequest search)
        {
            return new ContentSearchResult
            {
                Item = SearchItems(search),
            };
        }

        public ContentItemResult SearchItems(ContentSearchRequest search)
        {
            var item = _query.GetItem(search.Id, search.Language, search.PublishingTarget);
            if (item == null)
                return null;

            switch (search.GetDepth())
            {
                case ContentsSearchDepth.Item:
                    return _adapter.AdaptItem(item);
                case ContentsSearchDepth.Children:
                    return _adapter.AdaptItemWithChildren(item, _query.GetItemChildren(item));
                case ContentsSearchDepth.Descendants:
                    return _adapter.AdaptItemWithChildren(item, _query.GetItemDescendants(item));
                default:
                    throw new NotImplementedException($"{nameof(ContentsSearchDepth)} value {search.Depth} not implemented");
            }
        }
    }
}
