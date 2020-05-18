using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Persons;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Net;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonsController : Controller
    {
        private PersonService PersonService { get; }

        public PersonsController(PersonService personService)
        {
            PersonService = personService;
        }

        /// <summary>
        /// Get paginated collection of all persons.
        /// </summary> 
        [HttpGet]
        [AllowAnonymous]
        [OpenApiOperation(nameof(PersonsController) + "_" + nameof(GetPersonList))]
        [ProducesResponseType(typeof(PaginatedData<SimplePerson>), (int)HttpStatusCode.OK)]
        public IActionResult GetPersonList([FromQuery] PersonSearchRequest request)
        {
            var persons = PersonService.GetPersonList(request);
            return Ok(persons);
        }

        /// <summary>
        /// Create person.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "ContentManager,Administrator")]
        [OpenApiOperation(nameof(PersonsController) + "_" + nameof(CreatePerson))]
        [ProducesResponseType(typeof(SimplePerson), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreatePerson([FromBody] CreatePersonRequest request)
        {
            var person = PersonService.CreatePerson(request);
            return Ok(person);
        }

        /// <summary>
        /// Get all information about person.
        /// </summary>
        /// <param name="id">Unique ID of person.</param>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [OpenApiOperation(nameof(PersonsController) + "_" + nameof(GetPersonDetail))]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetPersonDetail(long id)
        {
            var person = PersonService.GetPersonDetail(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        /// <summary>
        /// Update of person.
        /// </summary>
        /// <param name="id">Unique ID of person.</param>
        /// <param name="request"></param>
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentManager,Administrator")]
        [OpenApiOperation(nameof(PersonsController) + "_" + nameof(UpdatePerson))]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdatePerson(long id, [FromBody] EditPersonRequest request)
        {
            var person = PersonService.UpdatePerson(id, request);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        /// <summary>
        /// Delete person.
        /// </summary>
        /// <param name="id">Unique ID of person.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "ContentManager,Administrator")]
        [OpenApiOperation(nameof(PersonsController) + "_" + nameof(DeletePerson))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeletePerson(long id)
        {
            var success = PersonService.DeletePerson(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Gets list of all persons as filtering data source.
        /// </summary>
        [HttpGet("filter")]
        [AllowAnonymous]
        [OpenApiOperation(nameof(PersonsController) + "_" + nameof(GetPersonsFilterData))]
        [ProducesResponseType(typeof(List<PersonFilterItem>), (int)HttpStatusCode.OK)]
        public IActionResult GetPersonsFilterData()
        {
            var persons = PersonService.GetPersonsFilterData();
            return Ok(persons);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                PersonService.Dispose();

            base.Dispose(disposing);
        }
    }
}