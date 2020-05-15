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
    public class PersonController : Controller
    {
        private readonly PersonFacade _personFacade;
        public PersonController(PersonFacade facade)
        {
            _personFacade = facade;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(RegisterViewModel personModel)
        {
            await _personFacade.InsertAsync(personModel.UserModel);
            return RedirectToAction(nameof(personModel));
        }
    }
}
