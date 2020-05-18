using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web.ViewModels;

namespace MovieDatabase.Web.Controllers
{
    public class SearchController : Controller
    {
        private protected SearchFacade _searchFacade;
        public SearchController(SearchFacade facade)
        {
            _searchFacade = facade;
        }

        [HttpGet]
        public async Task<IActionResult> Search(String search, int? page)
        {
            var searchViewModel = new SearchViewModel
            {
                SearchResultBase = await Find(search,page)
            };
            return View(searchViewModel);
        }

        [HttpGet]
        public async Task<PaginatedDataOfSearchResultBase> Find(String name, int? page)
        {
            return await _searchFacade.SearchAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, name, 10, page);
        }

    }
}