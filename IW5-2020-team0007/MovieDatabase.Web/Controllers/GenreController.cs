using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web.ViewModels;

namespace MovieDatabase.Web.Controllers
{
    public class GenreController : Controller
    {
        private protected GenreFacade _genreFacade;
        public GenreController(GenreFacade facade)
        {
            _genreFacade = facade;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new GenresViewModel()
            {
                Genres = (await _genreFacade.GetGenresListAsync(null)).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreRequest createGenre)
        {
            var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;
            await _genreFacade.CreateGenreAsync(token, createGenre);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var token = HttpContext.User.FindFirst(ClaimTypes.Hash).Value;
            await _genreFacade.DeleteGenreAsync(token, id);
            return RedirectToAction(nameof(Index));
        }
    }
}