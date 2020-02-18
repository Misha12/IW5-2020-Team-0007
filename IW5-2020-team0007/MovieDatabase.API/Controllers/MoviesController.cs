using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetMovies()
        {
            return StatusCode(501);
        }

        [HttpGet("{id}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult GetMovieById(long id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult CreateMovie(/*TODO: Parametry*/)
        {
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult DeleteMovie(long id)
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(HttpStatusCode.NotImplemented, typeof(NotImplementedException))]
        public IActionResult UpdateMovie(long id /*TODO: dalsi parametry z body*/)
        {
            return StatusCode(501);
        }
    }
}