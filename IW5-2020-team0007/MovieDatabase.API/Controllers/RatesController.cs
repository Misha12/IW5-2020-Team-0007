using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Models;
using MovieDatabase.API.Services;
using MovieDatabase.Domain.DTO;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies/{movieID}/rates")]
    public class RatesController : ControllerBase
    {
        private RatesService Service { get; }

        public RatesController(RatesService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get all rates for movie.
        /// </summary>
        /// <param name="movieID">Unique ID of movie.</param>
        /// <param name="scoreFrom">Optional parametr for minimal rate.</param>
        /// <param name="scoreTo">Optional parametr for maximal rate.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Rate>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieRates(long movieID, int? scoreFrom = null, int? scoreTo = null)
        {
            var list = Service.GetRateList(movieID, scoreFrom, scoreTo);

            if (list == null)
                return NotFound(new ErrorModel("Requested movie was not found."));

            return Ok(list);
        }

        /// <summary>
        /// Get rate with information about movie.
        /// </summary>
        /// <param name="movieID">Unique ID of movie.</param>
        /// <param name="rateID">Unique ID of rate.</param>
        /// <returns></returns>
        [HttpGet("{rateID}")]
        [ProducesResponseType(typeof(RateDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieRateByRateID(long movieID, long rateID)
        {
            var rate = Service.FindRateByID(movieID, rateID);
            return rate == null ? (IActionResult)NotFound(new ErrorModel("Requested movie rate was not found.")) : Ok(rate);
        }

        /// <summary>
        /// Create rate.
        /// </summary>
        /// <param name="movieID">Unique ID of movie.</param>
        /// <param name="data"></param>
        [HttpPost]
        [ProducesResponseType(typeof(RateDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult CreateMovieRate(long movieID, [FromBody] RateInput data)
        {
            if (!data.IsValid())
                return BadRequest(new ErrorModel("Rates description was not specified."));

            var rate = Service.CreateRate(movieID, data);
            return rate == null ? (IActionResult)NotFound(new ErrorModel("Movie rate couldn't be added, because the movie is not in database.")) : Ok(rate);
        }

        /// <summary>
        /// Change rate.
        /// </summary>
        /// <param name="movieID">Unique ID of movie.</param>
        /// <param name="rateID">Unique ID of rate.</param>
        /// <param name="data"></param>
        [HttpPut("{rateID}")]
        [ProducesResponseType(typeof(RateDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdateMovieRate(long movieID, long rateID, RateInput data)
        {
            var rate = Service.UpdateRate(movieID, rateID, data);
            return rate == null ? (IActionResult)NotFound(new ErrorModel("Movie rate couldn't be changed, because it's not in database.")) : Ok(rate);
        }

        /// <summary>
        /// Delete rate.
        /// </summary>
        /// <param name="movieID">Unique ID of movie.</param>
        /// <param name="rateID">Unique ID of rate.</param>
        [HttpDelete("{rateID}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteMovieName(long movieID, long rateID)
        {
            var success = Service.DeleteRate(movieID, rateID);
            return success ? Ok(null) : (IActionResult)NotFound(null);
        }

        /// <summary>
        /// Get all rates fo all movies.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/rates")]
        [ProducesResponseType(typeof(List<Rate>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllRates(int? scoreFrom = null, int? scoreTo = null)
        {
            return Ok(Service.GetRateList(null, scoreFrom, scoreTo));
        }
    }
}