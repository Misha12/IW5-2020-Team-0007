using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Movies;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : Controller
    {
        private MovieService Service { get; }

        public MoviesController(MovieService service)
        {
            Service = service;
        }

        /// <summary>
        /// Create movie.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "SubAdmin,Administrator")]
        [OpenApiOperation(nameof(MoviesController) + "_" + nameof(CreateMovie))]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateMovie([FromBody] CreateMovieRequest request)
        {
            var movie = Service.CreateMovie(request);
            return Ok(movie);
        }

        /// <summary>
        /// Get all information about movie.
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [OpenApiOperation(nameof(MoviesController) + "_" + nameof(GetMovieDetail))]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieDetail(long id)
        {
            var movie = Service.GetMovieDetail(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        /// <summary>
        /// Get dictionary od all movies.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [OpenApiOperation(nameof(MoviesController) + "_" + nameof(GetMoviesList))]
        [ProducesResponseType(typeof(PaginatedData<SimpleMovie>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetMoviesList([FromQuery] MovieSearchRequest request)
        {
            var movies = Service.GetMoviesList(request);
            return Ok(movies);
        }

        /// <summary>
        /// Update of movie.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "SubAdmin,Administrator")]
        [OpenApiOperation(nameof(MoviesController) + "_" + nameof(UpdateMovie))]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateMovie(long id, [FromBody] EditMovieRequest request)
        {
            var movie = Service.UpdateMovie(id, request);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        /*
        
        /// <summary>
        /// Delete movie.
        /// </summary>
        /// <param name="id">Unique ID of movie.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteMovie(long id)
        {
            var success = Service.DeleteMovie(id);
            return success ? Ok(null) : (IActionResult)NotFound(null);
        }
        */

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Service.Dispose();

            base.Dispose(disposing);
        }
    }
}