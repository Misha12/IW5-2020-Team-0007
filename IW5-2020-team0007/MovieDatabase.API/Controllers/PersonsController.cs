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
        /// Získání seznamu všech osob.
        /// </summary>
        /// <param name="search">Volitelný filtrační parametr nad jménem a příjmením.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Person>), (int)HttpStatusCode.OK)]
        public IActionResult GetPersons(string search = null)
        {
            var data = Service.GetPersons(search);
            return Ok(data);
        }

        /// <summary>
        /// Získání detailní informace o osobě.
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor osoby.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetPersonById(long id)
        {
            var person = Service.FindPersonByID(id);
            return person == null ? (IActionResult)NotFound(new ErrorModel("Osoba s požadovaným ID neexistuje")) : Ok(person);
        }

        /// <summary>
        /// Vytvoření osoby.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreatePerson([FromBody] PersonInput data)
        {
            if (!data.IsValid())
                return BadRequest(new ErrorModel("Požadovaná data jsou v nesprávném formátu."));

            var person = Service.CreatePerson(data);
            return Ok(person);
        }

        /// <summary>
        /// Smazání osoby.
        /// </summary>
        /// <param name="id">Jednoznačný identifikátor uživatele.</param>
        /// <remarks>Pozor. Uživatel bude odebrán ze všech filmů, ve kterých režíroval, nebo hrál.</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeletePerson(long id)
        {
            var success = Service.DeletePerson(id);
            return success ? Ok(null) : (IActionResult)NotFound(null);
        }

        /// <summary>
        /// Aktualizace osoby.
        /// </summary>
        /// <param name="id">Jedinečný identifikátor osoby.</param>
        /// <param name="data"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Person), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdatePerson(long id, PersonInput data)
        {
            var person = Service.UpdatePerson(id, data);
            return person == null ? NotFound(new ErrorModel("Požadovaná osoba nebyla nalezena.")) : (IActionResult)Ok(person);
        }
    }
}