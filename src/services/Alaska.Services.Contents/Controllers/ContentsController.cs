using Alaska.Services.Contents.Domain.Models.Search;
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
        private readonly ContentsServiceClient _contentsClient;

        public ContentsController(ContentsServiceClient contentsClient)
        {
            _contentsClient = contentsClient ?? throw new ArgumentNullException(nameof(contentsClient));
        }

        [HttpGet]
        public async Task<ActionResult<ContentSearchResult>> GetContents([FromQuery]ContentsSearchRequest searchRequest)
        {
            var content = await _contentsClient.GetContents(searchRequest);
            return Ok(content);
        }
    }
}
