using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Requests;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
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
        private readonly IContentsService _contentsService;

        public ContentsController(IContentsService contentsService)
        {
            _contentsService = contentsService ?? throw new ArgumentNullException(nameof(contentsService));
        }

        [HttpGet]
        public async Task<ActionResult<ContentSearchResult>> GetContent([FromQuery]ContentSearchRequest searchRequest)
        {
            return Ok(await _contentsService.GetContent(searchRequest));
        }

        [HttpPost]
        public async Task<ActionResult<ContentsSearchResult>> SearchContents([FromBody]ContentsSearchRequest searchRequest)
        {
            return Ok(await _contentsService.SearchContents(searchRequest));
        }

        [HttpPost]
        public async Task<ActionResult<ContentItem>> CreateContent([FromBody]ContentCreationRequest request)
        {
            return Ok(await _contentsService.CreateContent(request));
        }

        [HttpPost]
        public async Task<ActionResult<ContentItem>> UpdateContent([FromBody]ContentItem item)
        {
            return Ok(await _contentsService.UpdateContent(item));
        }

        [HttpPost]
        public async Task<ActionResult> PublishContent([FromBody]PublishContentRequest publishingRequest)
        {
            await _contentsService.PublishContent(publishingRequest);
            return Ok();
        }
    }
}
