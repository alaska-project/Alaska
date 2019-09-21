using Alaska.Services.Contents.Application.Commands;
using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Application.Services
{
    internal class ContentsService : IContentsService
    {
        private readonly IMediator _mediator;
        private readonly IContentQueries _contentQueries;

        public ContentsService(
            IMediator mediator,
            IContentQueries contentQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _contentQueries = contentQueries ?? throw new ArgumentNullException(nameof(contentQueries));
        }

        public async Task<ContentItem> CreateContent(ContentCreationRequest request)
        {
            return await _mediator.Send(new CreateContentCommand(request));
        }

        public async Task<ContentSearchResult> GetContent(ContentSearchRequest searchRequest)
        {
            return await _contentQueries.GetContent(searchRequest);
        }

        public async Task PublishContent(PublishContentRequest publishingRequest)
        {
            await _mediator.Send(new PublishContentCommand(publishingRequest));
        }

        public async Task<ContentsSearchResult> SearchContents(ContentsSearchRequest searchRequest)
        {
            return await _contentQueries.SearchContents(searchRequest);
        }

        public async Task<ContentItem> UpdateContent(ContentItem item)
        {
            return await _mediator.Send(new UpdateContentCommand(item));
        }
    }
}
