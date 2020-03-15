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
        /// Get dictionary of all genres.
        /// </summary>
        /// <param name="search">Optional argument for filter with name of genre.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Genre>), (int)HttpStatusCode.OK)]
        public IActionResult GetGenreList(string search = null)
        {
            var list = Service.GetFullList(search);
            return Ok(list);
        }

        /// <summary>
        /// Get genre by ID.
        /// </summary>
        /// <param name="id">Unique ID of genre.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetGenreByID(int id)
        {
            var genre = Service.FindGenreByID(id);
            return genre == null ? NotFound(new ErrorModel("Requested genre was not found.")) : (IActionResult)Ok(genre);
        }

        /// <summary>
        /// Create genre.
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
        /// Update of genre.
        /// </summary>
        /// <param name="id">Unique ID of genre.</param>
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
        /// Delete genre.
        /// </summary>
        /// <param name="id">Unique ID of genre</param>
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