using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies/{movieID}/rates")]
    public class RatesController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetMovieRates(long movieID)
        {
            return StatusCode(501);
        }

        [HttpGet("{rateId}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetMovieRateByRateId(long movieID, long rateId)
        {
            return StatusCode(501);
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult CreateMovieRate(long movieID /*TODO: Body parameters*/)
        {
            return StatusCode(501);
        }

        [HttpPut("{rateId}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult UpdateMovieRate(long movieID, long rateId /*TODO: Body parameter*/)
        {
            return StatusCode(501);
        }

        [HttpDelete("{rateId}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult DeleteMovieName(long movieID, long rateId)
        {
            return StatusCode(501);
        }

        [HttpGet("/rates")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetAllRates()
        {
            return StatusCode(501);
        }
    }
}