using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web.ViewModels;

namespace MovieDatabase.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly PersonFacade _personFacade;
        public UserController(PersonFacade facade)
        {
            _personFacade = facade;
        }
        [HttpGet]
        public IActionResult New()
        {
            var userNewViewModel = new RegisterViewModel
            {
                UserModel = new RegisterRequest()
            };
            return View(userNewViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(RegisterRequest personModel)
        {
            await _personFacade.InsertAsync(personModel);
            return RedirectToAction(nameof(New));
        }
    }
}
