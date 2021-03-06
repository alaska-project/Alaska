﻿using Alaska.Common.Diagnostics.Abstractions;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Caching;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Extensions.Contents.Contentful.Services;
using Alaska.Services.Contents.Domain.Models.Search;
using Contentful.Core.Models;
using Contentful.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Application.Query
{
    public class ContentQueries
    {
        private readonly IProfiler _profiler;
        private readonly ContentfulClientsFactory _factory;
        private readonly ContentTypesCache _contentTypesCache;

        public ContentQueries(
            IProfiler profiler,
            ContentfulClientsFactory factory,
            ContentTypesCache contentTypesCache)
        {
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _contentTypesCache = contentTypesCache ?? throw new ArgumentNullException(nameof(contentTypesCache));
        }

        public async Task<IEnumerable<ContentItemData>> SearchContentItems(ContentsSearchRequest contentsSearch)
        {
            return await SearchContentItems<ContentItemData>(contentsSearch);
        }

        private async Task<IEnumerable<T>> SearchContentItems<T>(ContentsSearchRequest contentsSearch)
        {
            var query = new QueryBuilder<T>();

            if (!string.IsNullOrEmpty(contentsSearch.Language))
                query = query.LocaleIs(contentsSearch.Language);

            if (!string.IsNullOrEmpty(contentsSearch.TemplateId))
                query = query.ContentTypeIs(contentsSearch.TemplateId);

            contentsSearch.Filters?
                .ToList()
                .ForEach(x => query = AddFilter(query, x));

            return await _factory.GetContentsClient(IsPreview(contentsSearch.PublishingTarget)).GetEntries(query);
        }

        private bool IsPreview(string target)
        {
            return target.Equals(PublishingTarget.Preview.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        private async Task<T> SearchContentItems<T>(ContentItemReference item, bool preview)
        {
            using (_profiler.Measure(nameof(GetContentItem)))
            {
                var query = new QueryBuilder<T>().LocaleIs(item.Locale);
                return await _factory.GetContentsClient(preview).GetEntry(item.Id, query);
            }
        }

        public async Task<ContentItemData> GetContentItem(ContentItemReference item, string target)
        {
            var t = (PublishingTarget)Enum.Parse(typeof(PublishingTarget), target, true);
            return await GetContentItem(item, t);
        }

        public async Task<ContentItemData> GetContentItem(ContentItemReference item, PublishingTarget target)
        {
            return await GetContentItem(item, target == PublishingTarget.Preview);
        }

        private async Task<ContentItemData> GetContentItem(ContentItemReference item, bool preview)
        {
            return await GetContentItem<ContentItemData>(item, preview);
        }

        private async Task<T> GetContentItem<T>(ContentItemReference item, bool preview)
        {
            using (_profiler.Measure(nameof(GetContentItem)))
            {
                var query = new QueryBuilder<T>().LocaleIs(item.Locale);
                return await _factory.GetContentsClient(preview).GetEntry(item.Id, query);
            }
        }

        public ContentType GetContentType(ContentItemData entry)
        {
            var contentTypeId = (string)entry["sys"].contentType.sys.id.Value.ToString();
            return GetContentType((string)contentTypeId);
        }

        public ContentType GetContentType(string contentTypeId)
        {
            return _contentTypesCache.RetreiveContentType(contentTypeId, () => GetContentTypeSync(contentTypeId));
        }

        private ContentType GetContentTypeSync(string contentTypeId)
        {
            var t = ReadContentType(contentTypeId);
            t.Wait();
            return t.Result;
        }

        private async Task<ContentType> ReadContentType(string contentTypeId)
        {
            using (_profiler.Measure(nameof(GetContentType)))
            {
                return await _factory.GetContentManagementClient().GetContentType(contentTypeId);
            }
        }

        private QueryBuilder<T> AddFilter<T>(QueryBuilder<T> query, ContentItemFieldFilter filter)
        {
            switch (filter.Operator)
            {
                case FieldFilterOperator.Equals:
                    return query.FieldEquals($"fields.{filter.Name}", filter.Value);
                case FieldFilterOperator.NotEquals:
                    return query.FieldDoesNotEqual($"fields.{filter.Name}", filter.Value);
                case FieldFilterOperator.Matches:
                    return query.FieldMatches($"fields.{filter.Name}", filter.Value);
                default:
                    throw new NotImplementedException($"Filter operator {filter.Operator} not implemented");
            }
        }

        private QueryBuilder<T> BuildQuery<T>(string language) => new QueryBuilder<T>().LocaleIs(language);
    }
}
