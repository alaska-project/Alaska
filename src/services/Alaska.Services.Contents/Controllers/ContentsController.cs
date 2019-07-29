using Alaska.Services.Contents.Application.Commands;
using Alaska.Services.Contents.Application.Queries;
using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alaska.Services.Contents.Controllers
{
    [Route("alaska/api/[controller]/[action]")]
    public class ContentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IContentQueries _contentQueries;

        public ContentsController(
            IMediator mediator,
            IContentQueries contentQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _contentQueries = contentQueries ?? throw new ArgumentNullException(nameof(contentQueries));
        }

        [HttpGet]
        public async Task<ActionResult<ContentSearchResult>> GetContent([FromQuery]ContentSearchRequest searchRequest)
        {
            return Ok(await _contentQueries.GetContent(searchRequest));
        }

        [HttpPost]
        public async Task<ActionResult<ContentSearchResult>> SearchContents([FromBody]ContentsSearchRequest searchRequest)
        {
            return Ok(await _contentQueries.SearchContents(searchRequest));
        }

        [HttpPost]
        public async Task<ActionResult<ContentItem>> CreateContent([FromBody]ContentCreationRequest request)
        {
            var content = await _mediator.Send(new CreateContentCommand(request));
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<ContentItem>> UpdateContent([FromBody]ContentItem item)
        {
            return Ok(await _mediator.Send(new UpdateContentCommand(item)));
        }

        [HttpPost]
        public async Task<ActionResult> PublishContent([FromBody]PublishContentRequest publishingRequest)
        {
            await _mediator.Send(new PublishContentCommand(publishingRequest));
            return Ok();
        }
    }
}
