using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDatabase.BL.Web.Facades;

namespace MovieDatabase.Web.Controllers
{
    public class RateController : Controller
    {
        private protected RateFacade _rateFacade;
        public RateController(RateFacade facade)
        {
            _rateFacade = facade;
        }

        [Authorize]
        [HttpPost]
        public async Task CreateRate(CreateRateRequest rateRequest)
        {
            await _rateFacade.CreateRateAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, rateRequest);
        }

        [Authorize]
        [HttpPost]
        public async Task UpdateRate(long ID,EditRatingRequest rateRequest)
        {
            await _rateFacade.UpdateRateAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID, rateRequest);
        }

        [Authorize]
        [HttpPost]
        public async Task DeleteRate(long ID)
        {
            await _rateFacade.DeleteRateAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
        }

        [Authorize]
        [HttpPost]
        public async Task<PaginatedDataOfRating> GetRatingList(IEnumerable<long> ID, String text, int? scoreFrom, int? scoreTo, int? limit, int? page)
        {
            var a = await _rateFacade.GetRatingsListAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID, text, scoreFrom, scoreTo, limit, page);
            return a;
        }
    }
}