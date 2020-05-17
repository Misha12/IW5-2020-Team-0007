using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieDatabase.API.Services;
using MovieDatabase.Common.Extensions;
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

        /// <summary>
        /// Gets paginated list of users registered in system.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(GetUsersList))]
        [ProducesResponseType(typeof(PaginatedData<SimpleUser>), (int)HttpStatusCode.OK)]
        public IActionResult GetUsersList([FromQuery] UserSearchRequest request)
        {
            var data = UsersService.GetUsersList(request);
            return Ok(data);
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(Register))]
        [ProducesResponseType(typeof(SimpleUser), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = await UsersService.RegisterAsync(request);
            return Ok(user);
        }

        /// <summary>
        /// Finish user registration.
        /// </summary>
        [HttpGet("register")]
        [AllowAnonymous]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(ConfirmRegistration))]
        [ProducesResponseType((int)HttpStatusCode.Redirect)]
        public IActionResult ConfirmRegistration([FromQuery] ConfirmRegisterRequest request)
        {
            UsersService.ConfirmRegister(request.Code);
            return Redirect(Config["RegisterReturnUrl"]);
        }

        /// <summary>
        /// Gets user detail.
        /// </summary>
        /// <param name="id">Unique ID of user.</param>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(GetUserDetail))]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetUserDetail(long id)
        {
            var user = UsersService.GetUserDetail(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Gets user detail of current logged user.
        /// </summary>
        [HttpGet("me")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(GetCurrentUserDetail))]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public IActionResult GetCurrentUserDetail()
        {
            var currentUserID = HttpContext.User.GetUserID();
            return Ok(UsersService.GetUserDetail(currentUserID));
        }

        /// <summary>
        /// Updates user.
        /// </summary>
        /// <param name="id">Unique ID of user.</param>
        /// <param name="request"></param>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(UpdateUser))]
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

        /// <summary>
        /// Updates current logged user.
        /// </summary>
        [HttpPut("me")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(UpdateCurrentUser))]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.OK)]
        public IActionResult UpdateCurrentUser([FromBody] UserEditRequest request)
        {
            var currentUserID = HttpContext.User.GetUserID();
            return Ok(UsersService.UpdateUser(currentUserID, request));
        }

        /// <summary>
        /// Changes user password.
        /// </summary>
        /// <param name="id">Unique ID of user.</param>
        /// <param name="request"></param>
        [HttpPut("{id}/password")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(ChangePassword))]
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

        /// <summary>
        /// Changes password of current logged user.
        /// </summary>
        /// <param name="request"></param>
        [HttpPut("me/password")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(ChangeCurrentUserPassword))]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult ChangeCurrentUserPassword([FromBody] PasswordChangeRequest request)
        {
            var currentUserID = HttpContext.User.GetUserID();
            return Ok(UsersService.ChangePassword(currentUserID, request));
        }

        /// <summary>
        /// Deletes a user. All ratings of user will be deleted.
        /// </summary>
        /// <param name="id">Unique ID of user.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(DeleteUser))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult DeleteUser(long id)
        {
            var success = UsersService.DeleteUser(id);

            if (!success)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Deletes current logged user.
        /// </summary>
        [HttpDelete("me")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(DeleteCurrentUser))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult DeleteCurrentUser()
        {
            var currentUserID = HttpContext.User.GetUserID();
            UsersService.DeleteUser(currentUserID);
            return Ok();
        }

        /// <summary>
        /// Changes role of user.
        /// </summary>
        /// <param name="id">Unique ID of user.</param>
        /// <param name="request"></param>
        [HttpPut("{id}/role")]
        [Authorize(Roles = "Administrator")]
        [OpenApiOperation(nameof(UsersController) + "_" + nameof(ChangeRole))]
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
                UsersService.Dispose();

            base.Dispose(disposing);
        }
    }
}