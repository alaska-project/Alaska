﻿using Alaska.Common.Diagnostics.Abstractions;
using Alaska.Extensions.Contents.Contentful.Application.Commands;
using Alaska.Extensions.Contents.Contentful.Application.Extensions;
using Alaska.Extensions.Contents.Contentful.Application.Query;
using Alaska.Extensions.Contents.Contentful.Converters;
using Alaska.Extensions.Contents.Contentful.Infrastructure.Clients;
using Alaska.Extensions.Contents.Contentful.Models;
using Alaska.Services.Contents.Domain.Exceptions;
using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Contentful.Core.Models;
using Contentful.Core.Search;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Extensions.Contents.Contentful.Services
{
    internal class ContentsService : IContentsService
    {
        private readonly IMediator _mediator;
        private readonly ContentQueries _query;
        private readonly IProfiler _profiler;
        private readonly ContentsConverter _converter;

        public ContentsService(
            IMediator mediator,
            ContentQueries query,
            IProfiler profiler,
            ContentsConverter converter)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _profiler = profiler ?? throw new ArgumentNullException(nameof(profiler));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public async Task<ContentItem> GetPreviewItem(string itemId, string language)
        {
            var result = await SearchContent(new ContentsSearchRequest
            {
                Id = itemId,
                Language = language,
                PublishingTarget = PublishingTarget.Preview.ToString(),
            });
            return result?.Item?.Value;
        }

        public async Task<ContentSearchResult> SearchContent(ContentsSearchRequest contentsSearch)
        {
            if (contentsSearch.GetDepth() != ContentsSearchDepth.Item)
                throw new UnsupportedFeatureException($"{contentsSearch.GetDepth()}  not supported by Contentful provider");

            var entry = await _query.GetContentItem(new ContentItemReference
            {
                Id = contentsSearch.Id,
                Locale = contentsSearch.Language,
            }, contentsSearch.PublishingTarget);

            var contentType = _query.GetContentType(entry);

            return ConvertToContentSearchResult(entry, contentType, contentsSearch.PublishingTarget);
        }

        public async Task<ContentItem> CreateContent(ContentCreationRequest creationRequest)
        {
            var command = new CreateContentCommand(creationRequest);
            await _mediator.Send(command);

            throw new NotImplementedException();
            //var updatedItem = await _query.GetContentItem(contentItem.GetReference(), contentItem.Info.PublishingTarget);
            //var contentType = _query.GetContentType(contentItem.Info.TemplateId);
            //return _converter.ConvertToContentItem(updatedItem, contentType, contentItem.Info.PublishingTarget);
        }

        public async Task<ContentItem> UpdateContent(ContentItem contentItem)
        {
            var command = new UpdateItemCommand(contentItem);
            await _mediator.Send(command);

            var updatedItem = await _query.GetContentItem(contentItem.GetReference(), contentItem.Info.PublishingTarget);
            var contentType = _query.GetContentType(contentItem.Info.TemplateId);
            return _converter.ConvertToContentItem(updatedItem, contentType, contentItem.Info.PublishingTarget);
        }

        public async Task PublishContent(PublishContentRequest contentPublish)
        {
            var command = new PublishContentCommand(contentPublish);
            await _mediator.Send(command);
        }

        private ContentSearchResult ConvertToContentSearchResult(ContentItemData entry, ContentType contentType, string target)
        {
            using (_profiler.Measure(nameof(ConvertToContentSearchResult)))
            {
                return new ContentSearchResult
                {
                    Item = new ContentItemResult
                    {
                        Value = _converter.ConvertToContentItem(entry, contentType, target),
                    },
                };
            }
        }
    }
}
