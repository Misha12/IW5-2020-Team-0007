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
        private GenreManagementService Service { get; }

        public GenresController(GenreManagementService service)
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
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult GetGenreById(int id)
        {
            var genre = Service.FindGenreById(id);
            return genre == null ? NotFound(null) : (IActionResult)Ok(genre);
        }

        /// <summary>
        /// Vytvoření žánru.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Dictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateGenre([FromBody] GenreInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = Service.CreateGenre(data.Name);
            return Ok(genre);
        }

        /// <summary>
        /// Aktualizace žánru.
        /// </summary>
        /// <param name="id">Jedinečný identifikátor žánru.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Dictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateGenre(int id, [FromBody] GenreInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = Service.UpdateGenre(id, data.Name);
            return genre == null ? NotFound(null) : (IActionResult)Ok(genre);
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