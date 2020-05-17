﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.BL.Web;
using MovieDatabase.BL.Web.Facades;
using MovieDatabase.Web.Models;
using MovieDatabase.Web.ViewModels;

namespace MovieDatabase.Web.Controllers
{
    public class UserController : Controller
    {
        private protected UserFacade _userFacade;
        private protected ClientFacade _clientFacade;
        public UserController(UserFacade facade, ClientFacade clientFacade)
        {
            _userFacade = facade;
            _clientFacade = clientFacade;
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
        /*
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginModel)
        {
            var a = await _userFacade.LoginAsync(loginModel);
            HttpContext.Session.SetString("AuthString", a.AccessToken);
            return RedirectToAction(nameof(Login));
        }*/
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginModel)
        {
            
            var a = await _clientFacade.LoginAsync(loginModel);
            
            User thisUser = await _userFacade.CurrentUserAsync(a.AccessToken);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, thisUser.Id.ToString()),
                new Claim(ClaimTypes.Name, thisUser.Username),
                new Claim(ClaimTypes.Email, thisUser.Email),
                new Claim(ClaimTypes.Role, thisUser.Role.ToString()),
                new Claim(ClaimTypes.Expiration, a.ExpiresAt.ToString()),
                new Claim(ClaimTypes.Hash, a.AccessToken),
                new Claim(ClaimTypes.SerialNumber, a.RefreshToken)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction(nameof(Login));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditCurrentUser(UserEditRequest userEditRequest)
        {
            await _userFacade.CurrentUserUpdate(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, userEditRequest);
            return RedirectToAction(nameof(Login));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshToken()
        {
            await _clientFacade.RefreshAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, HttpContext.User.FindFirst(ClaimTypes.SerialNumber).Value);
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public async Task<IActionResult> GetUsersListAsync()
        {
            var a = await GetUserSSListAsync();
            return RedirectToAction(nameof(Login));
        }
        [HttpPost]
        public async Task<PaginatedDataOfSimpleUser> GetUserSSListAsync()
        {
            var a = await _userFacade.GetUsersListAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, "f", 5, 1);
            return a;
        }
        public async Task DeleteCurrentAsync()
        {
            await _userFacade.DeleteUserAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
    }
}
