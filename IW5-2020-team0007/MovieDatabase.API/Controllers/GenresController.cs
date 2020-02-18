using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenresController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetGenreList()
        {
            return StatusCode(501);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult CreateGenre()
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id)
        {
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            return StatusCode(501);
        }
    }
}