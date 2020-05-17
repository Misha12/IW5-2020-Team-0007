using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web.Facades;

namespace MovieDatabase.Web.Controllers
{
    public class GenreController : Controller
    {
        private protected GenreFacade _genreFacade;
        public GenreController(GenreFacade facade)
        {
            _genreFacade = facade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewGenre(CreateGenreRequest createGenre)
        {

            await _genreFacade.CreateGenreAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, createGenre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGenre(String ID, EditGenreRequest editGenre)
        {

            await _genreFacade.UpdateGenreAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value,int.Parse(ID), editGenre);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGenre(String ID)
        {

            await _genreFacade.DeleteGenreAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, int.Parse(ID));
            return RedirectToAction(nameof(Index));
        }
    }
}