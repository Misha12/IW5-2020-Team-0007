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
        private readonly PersonFacade personFacade;

        public MovieController(MovieFacade facade, RateFacade _rateFacade, GenreFacade _genreFacade, PersonFacade _personFacade)
        {
            movieFacade = facade;
            rateFacade = _rateFacade;
            genreFacade = _genreFacade;
            personFacade = _personFacade;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieRequest movie)
        {
            await movieFacade.CreateMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, movie);
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
            try
            {
                var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;
                updateMovie.MovieRequest.Length = Convert.ToInt64(updateMovie.Length.TotalMinutes);

                await movieFacade.UpdateMovieAsync(token, updateMovie.ID, updateMovie.MovieRequest);
                TempData["SaveSuccess"] = true;
                return RedirectToAction(nameof(Detail), new { updateMovie.ID, page = 1 });
            }
            catch (ApiException)
            {
                // Catched on api side
                TempData["SaveSuccess"] = false;
                return RedirectToAction(nameof(Detail), new { updateMovie.ID, page = 1 });
            }
        }

<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> DeleteMovie(long ID)
        {
            await movieFacade.DeleteMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
            return RedirectToAction(nameof(HomeController.Index), "Home");
=======
        [HttpGet]
        public async Task<IActionResult> DeleteMovie(long ID)
        {
            await movieFacade.DeleteMovieAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
            return Redirect("/");
>>>>>>> 81de18c270035265a7963e39a00401c64a6fdcdb
        }

        [HttpGet]
        public async Task<IActionResult> List(int page = 1)
        {
            var MovieListViewModel = new MovieListViewModel()
            {
                listMovie = await GetMoviesList(null, null, null, null, null, 5, page)
            };

            return View(MovieListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(long ID, int page)
        {
            var token = HttpContext.User.FindFirst(ClaimTypes.Hash)?.Value;
            var ratings = await rateFacade.GetRatingsListAsync(token, new List<long> { ID }, null, null, null, 5, page);
            var genres = await genreFacade.GetGenresListAsync(null);
            var persons = await personFacade.GetPersonsFilterDataAsync();

            bool? saveSuccess = null;
            if (TempData.ContainsKey("SaveSuccess"))
            {
                saveSuccess = (bool)TempData["SaveSuccess"];
                TempData.Remove("SaveSuccess");
            }

            var MovieDetailViewModel = new MovieDetailViewModel()
            {
                DetailMovieModel = await GetMovieDetail(ID),
                ListRatingModel = ratings,
                Genres = genres.ToList(),
                Persons = persons,
                SaveSuccess = saveSuccess
            };

            return View(MovieDetailViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateActors(long ID, List<long> actors, long? toRemoveActor)
        {
            try
            {
                var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;

                if (toRemoveActor != null)
                    actors.Remove(toRemoveActor.Value);

                var request = new EditMovieRequest() { Actors = actors };
                await movieFacade.UpdateMovieAsync(token, ID, request);
                TempData["SaveSuccess"] = true;
                return RedirectToAction(nameof(Detail), new { ID, page = 1 });
            }
            catch (ApiException)
            {
                // Catched on api side
                TempData["SaveSuccess"] = false;
                return RedirectToAction(nameof(Detail), new { ID, page = 1 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDirectors(long ID, List<long> directors, long? toRemoveDirector)
        {
            try
            {
                var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;

                if (toRemoveDirector != null)
                    directors.Remove(toRemoveDirector.Value);

                var request = new EditMovieRequest() { Directors = directors };
                await movieFacade.UpdateMovieAsync(token, ID, request);
                TempData["SaveSuccess"] = true;
                return RedirectToAction(nameof(Detail), new { ID, page = 1 });
            }
            catch (ApiException)
            {
                // Catched on api side
                TempData["SaveSuccess"] = false;
                return RedirectToAction(nameof(Detail), new { ID, page = 1 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRate(long ID, string text, bool anonymous, int score)
        {
            try
            {
                var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;

                var rateRequest = new CreateRateRequest()
                {
                    Anonymous = anonymous,
                    MovieID = ID,
                    Score = score,
                    Text = text
                };

                await rateFacade.CreateRateAsync(token, rateRequest);
                return RedirectToAction(nameof(Detail), new { ID, page = 1 });
            }
            catch (ApiException)
            {
                // Catched on api side
                return RedirectToAction(nameof(Detail), new { ID, page = 1 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRate(long id, long movieId)
        {
            var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;
            await rateFacade.DeleteRateAsync(token, id);
            return RedirectToAction(nameof(Detail), new { ID = movieId, page = 1 });
        }

        [HttpGet]
        public async Task<IActionResult> ClearAllGenres(long id)
        {
            try
            {
                var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;

                var request = new EditMovieRequest() { GenreIds = new List<int>() };

                await movieFacade.UpdateMovieAsync(token, id, request);
                TempData["SaveSuccess"] = true;
                return RedirectToAction(nameof(Detail), new { id, page = 1 });
            }
            catch (ApiException)
            {
                // Catched on api side
                TempData["SaveSuccess"] = false;
                return RedirectToAction(nameof(Detail), new { id, page = 1 });
            }
        }
    }
}