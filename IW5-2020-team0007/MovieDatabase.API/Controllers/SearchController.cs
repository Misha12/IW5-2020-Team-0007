using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Search;
using NSwag.Annotations;
using System.Net;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : Controller
    {
        private SearchService Service { get; }

        public SearchController(SearchService service)
        {
            Service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        [OpenApiOperation(nameof(SearchController) + "_" + nameof(Search))]
        [ProducesResponseType(typeof(PaginatedData<SearchResultBase>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Search([FromQuery] SearchRequest request)
        {
            var result = Service.Search(request);
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Service.Dispose();

            base.Dispose(disposing);
        }
    }
}