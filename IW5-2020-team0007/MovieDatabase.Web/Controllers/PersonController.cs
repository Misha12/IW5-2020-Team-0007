using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web.Facades;

namespace MovieDatabase.Web.Controllers
{
    public class PersonController : Controller
    {
        private protected PersonFacade _personFacade;
        public PersonController(PersonFacade facade)
        {
            _personFacade = facade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPerson(CreatePersonRequest createPerson)
        {

            await _personFacade.CreatePersonAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, createPerson);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePerson(String ID, EditPersonRequest editPerson)
        {

            await _personFacade.UpdatePersonAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID, editPerson);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePerson(String ID)
        {

            await _personFacade.DeletePersonAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<Person> GetPersonDetail(String ID)
        {

            var a = await _personFacade.GetPersonDetailAsync(ID);
            return a;
        }
    }
}