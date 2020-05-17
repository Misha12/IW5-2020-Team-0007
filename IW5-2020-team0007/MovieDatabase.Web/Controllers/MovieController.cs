using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web.ViewModels;

namespace MovieDatabase.Web.Controllers
{
    public class MovieController : Controller
    {
        private protected MovieFacade movieFacade;
        public MovieController(MovieFacade facade)
        {
            movieFacade = facade;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieRequest movie)
        {
            await movieFacade.CreateMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value,movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<PaginatedDataOfSimpleMovie> GetMoviesList(String name, IEnumerable<int> genresIds, String country, long? lengthFrom, long? lengthTo, int? limit, int? page)
        {
            var a = await movieFacade.GetMoviesListAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, name, genresIds, country, lengthFrom, lengthTo, limit, page);
            return a;
        }

        [HttpPost]
        public async Task<Movie> GetMovieDetail(long ID)
        {
            var a = await movieFacade.GetMovieDetailAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
            return a;
        }

        [HttpPost]
        public async Task<Movie> UpdateMovie(MovieUpdateViewModel updateMovie)
        {
            var a = await movieFacade.UpdateMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, updateMovie.ID, updateMovie.MovieRequest);
            return a;
        }

        [HttpPost]
        public async Task DeleteMovie(long ID)
        {
            await movieFacade.DeleteMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
        }

        [HttpPost]
        public async Task<IActionResult> List(int page)
        {

            var MovieListViewModel = new MovieListViewModel()
            {
                listMovie = await GetMoviesList(null,null,null,null,null,5,page)
            };
            return View(MovieListViewModel);
        }
    }
}