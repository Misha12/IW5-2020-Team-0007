using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies/{movieID}/names")]
    public class MovieNamesController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetMovieNames(long movieID)
        {
            return StatusCode(501);
        }

        [HttpGet("{lang}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetMovieNameForLanguage(long movieID, string lang)
        {
            return StatusCode(501);
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult CreateMovieName(long movieID /*TODO: Body parameters*/)
        {
            return StatusCode(501);
        }

        [HttpPut("{lang}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult UpdateMovieName(long movieID, string lang /*TODO: Body parameter*/)
        {
            return StatusCode(501);
        }

        [HttpDelete("{lang}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult DeleteMovieName(long movieID, string lang)
        {
            return StatusCode(501);
        }
    }
}