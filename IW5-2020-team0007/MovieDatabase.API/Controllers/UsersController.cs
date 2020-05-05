using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Users;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : Controller
    {
        private UsersService UsersService { get; }
        private IConfiguration Config { get; }

        public UsersController(UsersService usersService, IConfiguration config)
        {
            UsersService = usersService;
            Config = config;
        }

        [HttpGet]
        [OpenApiOperation("getUsersList")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(typeof(PaginatedData<SimpleUser>), (int)HttpStatusCode.OK)]
        public IActionResult GetUsersList([FromQuery] UserSearchRequest request)
        {
            var data = UsersService.GetUsersList(request);
            return Ok(data);
        }

        [HttpPost]
        [AllowAnonymous]
        [OpenApiOperation("register")]
        [ProducesResponseType(typeof(SimpleUser), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await UsersService.RegisterAsync(request);
            return Ok(user);
        }

        [HttpGet("register")]
        [AllowAnonymous]
        [OpenApiOperation("confirmRegistration")]
        [ProducesResponseType((int)HttpStatusCode.Redirect)]
        public IActionResult ConfirmRegistration([FromQuery] ConfirmRegisterRequest request)
        {
            UsersService.ConfirmRegister(request.Code);
            return Redirect(Config["RegisterReturnUrl"]);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation("getUserDetail")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUserDetail(long id)
        {
            var user = UsersService.GetUserDetail(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("me")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation("getCurrentUserDetail")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public IActionResult GetCurrentUserDetail()
        {
            var currentUserID = Convert.ToInt64(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return Ok(UsersService.GetUserDetail(currentUserID));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation("updateUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult UpdateUser(long id, [FromBody] UserEditRequest request)
        {
            var user = UsersService.UpdateUser(id, request);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("me")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation("updateCurrentUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.OK)]
        public IActionResult UpdateCurrentUser([FromBody] UserEditRequest request)
        {
            var currentUserID = Convert.ToInt64(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return Ok(UsersService.UpdateUser(currentUserID, request));
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation("changePassword")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult ChangePassword(long id, [FromBody] PasswordChangeRequest request)
        {
            var user = UsersService.ChangePassword(id, request);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("me/password")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation("changeCurrentUserPassword")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult ChangeCurrentUserPassword([FromBody] PasswordChangeRequest request)
        {
            var currentUserID = Convert.ToInt64(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return Ok(UsersService.ChangePassword(currentUserID, request));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation("deleteUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeleteUser(long id)
        {
            var success = UsersService.DeleteUser(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        [HttpDelete("me")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation("deleteCurrentUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteCurrentUser()
        {
            var currentUserID = Convert.ToInt64(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            UsersService.DeleteUser(currentUserID);
            return Ok();
        }

        [HttpPut("{id}/role")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation("changeRole")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult ChangeRole(long id, [FromBody] RoleChangeRequest request)
        {
            var user = UsersService.ChangeUserRole(id, request);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UsersService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}