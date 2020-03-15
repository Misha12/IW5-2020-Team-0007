using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Models;
using MovieDatabase.API.Services;
using MovieDatabase.Domain.DTO;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : ControllerBase
    {
        private PersonService Service { get; }

        public PersonsController(PersonService service)
        {
            Service = service;
        }

        /// <summary>
        /// Get collection of all persons.
        /// </summary>
        /// <param name="search">Optional parametr for movie name and surname.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Person>), (int)HttpStatusCode.OK)]
        public IActionResult GetPersons(string search = null)
        {
            var data = Service.GetPersons(search);
            return Ok(data);
        }

        /// <summary>
        /// Get all information about person.
        /// </summary>
        /// <param name="id">Unique ID of person.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetPersonByID(long id)
        {
            var person = Service.FindPersonByID(id);
            return person == null ? (IActionResult)NotFound(new ErrorModel("Person with this ID is not in database.")) : Ok(person);
        }

        /// <summary>
        /// Create person.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreatePerson([FromBody] PersonInput data)
        {
            if (!data.IsValid())
                return BadRequest(new ErrorModel("Requested data are in wrong format."));

            var person = Service.CreatePerson(data);
            return Ok(person);
        }

        /// <summary>
        /// Delete person.
        /// </summary>
        /// <param name="id">Unique ID of person.</param>
        /// <remarks>Warning. Peson will be deleted from all movies on which he/she participated.</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeletePerson(long id)
        {
            var success = Service.DeletePerson(id);
            return success ? Ok(null) : (IActionResult)NotFound(null);
        }

        /// <summary>
        /// Update of person.
        /// </summary>
        /// <param name="id">Unique ID of person.</param>
        /// <param name="data"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdatePerson(long id, PersonInput data)
        {
            var person = Service.UpdatePerson(id, data);
            return person == null ? NotFound(new ErrorModel("Requested person was not found.")) : (IActionResult)Ok(person);
        }
    }
}