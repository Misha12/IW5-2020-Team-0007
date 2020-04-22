using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Users;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [Authorize]
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UsersService UsersService { get; }

        public UsersController(UsersService usersService)
        {
            UsersService = usersService;
        }

        [HttpGet]
        [OpenApiOperation("getUsersList")]
        [ProducesResponseType(typeof(PaginatedData<SimpleUser>), (int)HttpStatusCode.OK)]
        public IActionResult GetUsersList([FromQuery] UserSearchRequest request)
        {
            var data = UsersService.GetUsersList(request);
            return Ok(data);
        }
    }
}