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
        private readonly UserFacade _userFacade;
        public UserController(UserFacade facade)
        {
            _userFacade = facade;
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

        [HttpGet]
        public IActionResult Login()
        {
            var loginViewModel = new LoginViewModel()
            {
                LoginModel = new LoginRequest()
            };
            return View(loginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(RegisterRequest personModel)
        {
            await _userFacade.InsertAsync(personModel);
            return RedirectToAction(nameof(New));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginModel)
        {
            await _userFacade.LoginAsync(loginModel);
            return RedirectToAction(nameof(Login));
        }
    }
}
