using Alaska.Services.Contents.Domain.Models.Items;
using Alaska.Services.Contents.Domain.Models.Search;
using Sitecore.Plugins.Alaska.Contents.Commands;
using Sitecore.Plugins.Alaska.Contents.Filters;
using Sitecore.Plugins.Alaska.Contents.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Sitecore.Plugins.Alaska.Contents.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContentsController : ApiController
    {
        private readonly ItemSearchService _searchService = new ItemSearchService();

        [HttpGet]
        [ResponseType(typeof(ContentSearchResult))]
        public IHttpActionResult GetContents([FromUri]ContentsSearchRequest searchRequest)
        {
            return Ok(_searchService.Search(searchRequest));
        }

        [HttpPost]
        [SitecoreEditorAuthorize]
        [ResponseType(typeof(ContentItem))]
        public IHttpActionResult UpdateContent([FromBody]ContentItem item)
        {
            var command = new UpdateItemCommand(item);
            var result = new UpdateItemCommandHandler().Handle(command);
            return Ok(result);
        }
    }
}
