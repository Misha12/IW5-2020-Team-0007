using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public async Task<IActionResult> List(int page)
        {

            var UserListViewModel = new UserListViewModel()
            {
                listUser = await GetUserSSListAsync(page)
            };
            return View(UserListViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail()
        {

            var DetailUserViewModel = new DetailUserViewModel()
            {
                UserModel = await CurrentUser()
            };
            return View(DetailUserViewModel);
        }

        [HttpGet]
        public async Task<User> CurrentUser()
        {
            return await _userFacade.CurrentUserAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value);
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

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties()
                {
                    ExpiresUtc = a.ExpiresAt,
                    IssuedUtc = DateTimeOffset.UtcNow
                });

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

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
        /*
        [HttpPost]
        public async Task<IActionResult> GetUsersListAsync()
        {
            var a = await GetUserSSListAsync();
            return RedirectToAction(nameof(Login));
        }*/

        [HttpPost]
        public async Task<PaginatedDataOfSimpleUser> GetUserSSListAsync(int page)
        {
            var a = await _userFacade.GetUsersListAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, null, 5, page);
            return a;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeMyPassword(PasswordChangeRequest userPassViewModel)
        {
            var a = await _userFacade.ChangeCurrentUserPasswordAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, userPassViewModel);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<User> ChangePassword(long ID,UserPassViewModel userPassViewModel)
        {
            var a = await _userFacade.ChangePasswordAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID,userPassViewModel.PasswordModel);
            return a;
        }

        [HttpPost]
        public async Task<User> ChangeRole(long ID, RoleChangeRequest roleChange)
        {
            var a = await _userFacade.ChangeRoleAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, ID, roleChange);
            return a;
        }

        [HttpPost]
        public async Task DeleteCurrentAsync()
        {
            await _userFacade.DeleteUserAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value, long.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }

        [HttpPost]
        public async Task<User> GetUserDetail(long ID)
        {
            return await _userFacade.GetUserDetailAsync(HttpContext.User.FindFirst(ClaimTypes.Hash).Value,ID);
        }

    }
}
