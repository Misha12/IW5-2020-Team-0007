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
        private protected RateFacade rateFacade;
        private readonly GenreFacade genreFacade;

        public MovieController(MovieFacade facade, RateFacade _rateFacade, GenreFacade _genreFacade)
        {
            movieFacade = facade;
            rateFacade = _rateFacade;
            genreFacade = _genreFacade;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieRequest movie)
        {
            await movieFacade.CreateMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value,movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult New()
        {
            var movieNewViewModel = new MovieNewViewModel
            {
                MovieModel = new CreateMovieRequest()
            };
            return View(movieNewViewModel);
        }

        [HttpPost]
        public async Task<PaginatedDataOfSimpleMovie> GetMoviesList(String name, IEnumerable<int> genresIds, String country, long? lengthFrom, long? lengthTo, int? limit, int? page)
        {
            var a = await movieFacade.GetMoviesListAsync(name, genresIds, country, lengthFrom, lengthTo, limit, page);
            return a;
        }

        [HttpPost]
        public async Task<Movie> GetMovieDetail(long ID)
        {
            var a = await movieFacade.GetMovieDetailAsync(ID);
            return a;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovie(MovieUpdateViewModel updateMovie)
        {
            await movieFacade.UpdateMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, updateMovie.ID, updateMovie.MovieRequest);
            return RedirectToAction(nameof(HomeController.Index), "Home");
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

        [HttpPost]
        public async Task<IActionResult> Detail(long ID, int page)
        {
            var token = HttpContext.User.FindFirst(ClaimTypes.Hash)?.Value;
            var ratings = await rateFacade.GetRatingsListAsync(token, new List<long> { ID }, null, null, null, 10, page);
            var genres = await genreFacade.GetGenresListAsync(null);

            var MovieDetailViewModel = new MovieDetailViewModel()
            {
                DetailMovieModel = await GetMovieDetail(ID),
                ListRatingModel = ratings,
                Genres = genres.ToList()
            };
            
            return View(MovieDetailViewModel);
        }
    }
}