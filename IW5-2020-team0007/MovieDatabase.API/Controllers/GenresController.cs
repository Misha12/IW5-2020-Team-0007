using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Models;
using MovieDatabase.API.Services;
using MovieDatabase.Domain.DTO;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenresController : ControllerBase
    {
        private GenreService Service { get; }

        public GenresController(GenreService service)
        {
            Service = service;
        }

        /// <summary>
        /// Získá seznam všech žánrů.
        /// </summary>
        /// <param name="search">Volitelný filtrační parametr nad názvem žánru.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Genre>), (int)HttpStatusCode.OK)]
        public IActionResult GetGenreList(string search = null)
        {
            var list = Service.GetFullList(search);
            return Ok(list);
        }

        /// <summary>
        /// Získání žánru na základě ID.
        /// </summary>
        /// <param name="id">Jedinečný identifikátor žánru.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetGenreByID(int id)
        {
            var genre = Service.FindGenreByID(id);
            return genre == null ? NotFound(new ErrorModel("Requested genre was not found.")) : (IActionResult)Ok(genre);
        }

        /// <summary>
        /// Vytvoření žánru.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateGenre([FromBody] GenreInput data)
        {
            if (!data.IsValid())
                return BadRequest(new ErrorModel("Name of genre is in wrong format."));

            var genre = Service.CreateGenre(data.Name);
            return Ok(genre);
        }

        /// <summary>
        /// Aktualizace žánru.
        /// </summary>
        /// <param name="id">Jedinečný identifikátor žánru.</param>
        /// <param name="data"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GenreDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateGenre(int id, [FromBody] GenreInput data)
        {
            if (!data.IsValid())
                return BadRequest(new ErrorModel("New name of genre is in wrong format."));

            var genre = Service.UpdateGenre(id, data.Name);
            return genre == null ? NotFound(new ErrorModel("Requested genre was not found.")) : (IActionResult)Ok(genre);
        }

        /// <summary>
        /// Smazání žánru.
        /// </summary>
        /// <param name="id">Jedinečný identifikátor žánru.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteGenre(int id)
        {
            var success = Service.DeleteGenre(id);
            return success ? Ok() : (IActionResult)NotFound(null);
        }
    }
}