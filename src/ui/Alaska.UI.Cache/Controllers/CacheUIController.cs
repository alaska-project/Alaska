using Alaska.UI.Cache.Settings;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alaska.UI.Cache.Controllers
{
    [EnableCors("AllowAnyOrigin")]
    [Route("alaska/api/cacheUi/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CacheUIController : Controller
    {
        protected CacheUIOptions Options => CacheUIOptionsRepository.Options;

        [HttpGet]
        [Produces(typeof(IEnumerable<string>))]
        public IActionResult GetEndpoints()
        {
            return Ok(GetEndpointAddresses());
        }

        private IEnumerable<string> GetEndpointAddresses()
        {
            if (Options.Endpoints.Any())
                return Options.Endpoints;

            return new List<string> { $"{Request.Scheme}://{Request.Host}" };
        }
    }
}
