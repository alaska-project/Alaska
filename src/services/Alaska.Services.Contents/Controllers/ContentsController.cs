using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Publishing;
using Alaska.Services.Contents.Domain.Models.Search;
using Alaska.Services.Contents.Infrastructure.Abstractions;
using Alaska.Services.Contents.Infrastructure.Services;
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
        public async Task<ActionResult<ContentSearchResult>> GetContents([FromQuery]ContentsSearchRequest searchRequest)
        {
            var content = await _contentsService.SearchContent(searchRequest);
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<ContentItem>> UpdateContent([FromBody]ContentItem item)
        {
            var content = await _contentsService.UpdateContent(item);
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult> PublishContent([FromQuery]PublishContentRequest publishingRequest)
        {
            await _contentsService.PublishContent(publishingRequest);
            return Ok();
        }
    }
}
