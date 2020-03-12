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
        /// Získání filmu podle identifkátoru.
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor filmu.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieById(long id)
        {
            var data = Service.FindMovieById(id);
            return data == null ? NotFound(null) : (IActionResult)Ok(data);
        }

        /// <summary>
        /// Založení filmu.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Dictionary<string, string>), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateMovie([FromBody] MovieInput data)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = Service.CreateMovie(data.OriginalName, data.Genre, data.Length, data.Country, data.Description);
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
        /// <param name="name">Nový název filmu.</param>
        /// <param name="genreID">Nové ID žánru.</param>
        /// <param name="length">Nová délka filmu.</param>
        /// <param name="country">Nová země původu.</param>
        /// <param name="description">Nový popis.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Genre), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdateMovie(long id, string name, int? genreID = null, long? length = null, string country = null, string description = null)
        {
            var movie = Service.UpdateMovie(id, name, genreID, length, country, description);
            return movie == null ? NotFound(null) : (IActionResult)Ok(movie);
        }
    }
}