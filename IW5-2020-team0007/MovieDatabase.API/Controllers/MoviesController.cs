using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Models;
using MovieDatabase.API.Services;
using MovieDatabase.Domain.DTO;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private MovieService Service { get; }

        public MoviesController(MovieService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get dictionary od all movies.
        /// </summary>
        /// <param name="searchName">Optional parametr for movie name.</param>
        /// <param name="genres">Collection of movies IDs.</param>
        /// <param name="lengthFrom">Minimal lenght.</param>
        /// <param name="lengthTo">Maximal lenght.</param>
        /// <param name="countries">Collection of countries.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Movie>), (int)HttpStatusCode.OK)]
        public IActionResult GetMovies(string searchName = null, [FromQuery] int[] genres = null, long? lengthFrom = null, long? lengthTo = null, [FromQuery] string[] countries = null)
        {
            var data = Service.GetMovieList(searchName, genres, lengthFrom, lengthTo, countries);
            return Ok(data);
        }

        /// <summary>
        /// Get all information about movie.
        /// </summary>
        /// <param name="id">Unique ID of movie.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieByID(long id)
        {
            var data = Service.FindMovieByID(id);
            return data == null ? NotFound(new ErrorModel("Requested movie was not found.")) : (IActionResult)Ok(data);
        }

        /// <summary>
        /// Create movie.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MovieDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateMovie([FromBody] MovieInput data)
        {
            if (!data.IsValid(out string message))
                return BadRequest(new ErrorModel(message));

            var movie = Service.CreateMovie(data);
            return Ok(movie);
        }

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

        /// <summary>
        /// Update of movie.
        /// </summary>
        /// <param name="id">Unique ID of movie.</param>
        /// <param name="data"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MovieDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdateMovie(long id, [FromBody] MovieInput data)
        {
            var movie = Service.UpdateMovie(id, data);
            return movie == null ? NotFound(new ErrorModel("Requested movie was not found.")) : (IActionResult)Ok(movie);
        }
    }
}