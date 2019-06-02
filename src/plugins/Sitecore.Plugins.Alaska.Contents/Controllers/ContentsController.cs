using Alaska.Services.Contents.Domain.Models.Search;
using Sitecore.Plugins.Alaska.Contents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Sitecore.Plugins.Alaska.Contents.Controllers
{
    [Route("alaska/api/contents")]
    public class ContentsController : ApiController
    {
        private readonly ItemSearchService _searchService = new ItemSearchService();

        [HttpGet]
        [ActionName("getContents")]
        [ResponseType(typeof(ContentSearchResult))]
        public IHttpActionResult GetContents([FromUri]ContentsSearchRequest searchRequest)
        {
            return Ok(_searchService.Search(searchRequest));
        }
    }
}
