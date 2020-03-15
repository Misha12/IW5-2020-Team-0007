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
        /// Získání seznamu všech dostupných filmů.
        /// </summary>
        /// <param name="searchName">Volitelný vyhledávací parametr pro název filmu.</param>
        /// <param name="genres">Kolekce identifikátorů žánrů.</param>
        /// <param name="lengthFrom">Minimální délka</param>
        /// <param name="lengthTo">Maximální délka</param>
        /// <param name="countries">Kolekce zemí původů.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Movie>), (int)HttpStatusCode.OK)]
        public IActionResult GetMovies(string searchName = null, [FromQuery] int[] genres = null, long? lengthFrom = null, long? lengthTo = null, [FromQuery] string[] countries = null)
        {
            var data = Service.GetMovieList(searchName, genres, lengthFrom, lengthTo, countries);
            return Ok(data);
        }

        /// <summary>
        /// Získání detailních informací o filmu.
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor filmu.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieByID(long id)
        {
            var data = Service.FindMovieByID(id);
            return data == null ? NotFound(new ErrorModel("Requested movie was not found.")) : (IActionResult)Ok(data);
        }

        /// <summary>
        /// Založení filmu.
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
        /// Smazání filmu.
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor filmu.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteMovie(long id)
        {
            var success = Service.DeleteMovie(id);
            return success ? Ok(null) : (IActionResult)NotFound(null);
        }

        /// <summary>
        /// Úprava filmu.
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor filmu.</param>
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