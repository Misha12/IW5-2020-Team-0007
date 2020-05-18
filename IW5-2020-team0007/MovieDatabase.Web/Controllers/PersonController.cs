using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web.ViewModels;

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

        [HttpGet]
        public async Task<IActionResult> List(int page)
        {

            var MovieListViewModel = new PersonListViewModel()
            {
                listPerson = await GetPersonList(null, 5, page)
            };
            return View(MovieListViewModel);
        }
        [HttpGet]
        public IActionResult New()
        {
            var personNewViewModel = new PersonNewViewModel
            {
                PersonModel = new CreatePersonRequest()
            };
            return View(personNewViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string ID)
        {
            var PersonDetailViewModel = new PersonDetailViewModel
            {
                PersonDetailModel = await GetPersonDetail(ID)
            };
            return View(PersonDetailViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> NewPerson(CreatePersonRequest createPerson)
        {

            await _personFacade.CreatePersonAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, createPerson);
            return RedirectToAction(nameof(New));
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePerson(PersonUpdateViewModel personUpdate)
        {

            await _personFacade.UpdatePersonAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, personUpdate.ID.ToString(), personUpdate.EditPerson);
            return RedirectToAction("Detail",new { ID = personUpdate.ID});
        }

        [HttpPost]
        public async Task<IActionResult> DeletePerson(String ID)
        {
            await _personFacade.DeletePersonAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public async Task<Person> GetPersonDetail(String ID)
        {
            var a = await _personFacade.GetPersonDetailAsync(ID);
            return a;
        }

        [HttpPost]
        public async Task<PaginatedDataOfSimplePerson> GetPersonList(String nameSurname, int? limit, int? page)
        {
            var a = await _personFacade.GetPersonListAsync(nameSurname, limit, page);
            return a;
        }
    }
}