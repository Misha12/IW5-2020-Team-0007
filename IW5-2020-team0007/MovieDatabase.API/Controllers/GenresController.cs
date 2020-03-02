using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        public IActionResult GetGenreById(int id)
        {
            var genre = Service.FindGenreById(id);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult CreateGenre([FromBody] GenreInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = Service.CreateGenre(data.Name);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateGenre(int id, [FromBody] GenreInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var genre = Service.UpdateGenre(id, data.Name);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var success = Service.DeleteGenre(id);

            return success ? Ok() : (IActionResult)NotFound();
        }
    }
}