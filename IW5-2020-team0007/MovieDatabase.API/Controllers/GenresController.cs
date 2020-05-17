using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Models.Genres;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("genres")]
    [Authorize(Roles = "ContentManager,Administrator")]
    public class GenresController : Controller
    {
        private GenreService GenreService { get; }

        public GenresController(GenreService service)
        {
            GenreService = service;
        }

        /// <summary>
        /// Get dictionary of all genres.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [OpenApiOperation(nameof(GenresController) + "_" + nameof(GetGenresList))]
        [ProducesResponseType(typeof(List<Genre>), (int)HttpStatusCode.OK)]
        public IActionResult GetGenresList([FromQuery] GenresSearchRequest request)
        {
            var list = GenreService.GetFullList(request);
            return Ok(list);
        }

        /// <summary>
        /// Create genre.
        /// </summary>
        [HttpPost]
        [OpenApiOperation(nameof(GenresController) + "_" + nameof(CreateGenre))]
        [ProducesResponseType(typeof(SimpleGenre), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateGenre([FromBody] CreateGenreRequest request)
        {
            var genre = GenreService.CreateGenre(request);
            return Ok(genre);
        }

        /// <summary>
        /// Update of genre.
        /// </summary>
        [HttpPut("{id}")]
        [OpenApiOperation(nameof(GenresController) + "_" + nameof(UpdateGenre))]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateGenre(int id, [FromBody] EditGenreRequest request)
        {
            var genre = GenreService.UpdateGenre(id, request);

            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        /// <summary>
        /// Delete genre.
        /// </summary>
        /// <param name="id">Unique ID of genre</param>
        [HttpDelete("{id}")]
        [OpenApiOperation(nameof(GenresController) + "_" + nameof(DeleteGenre))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeleteGenre(int id)
        {
            var success = GenreService.DeleteGenre(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                GenreService.Dispose();

            base.Dispose(disposing);
        }
    }
}