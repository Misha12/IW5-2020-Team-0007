using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Auth;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [Authorize]
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService AuthService { get; }

        public AuthController(AuthService authService)
        {
            AuthService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [OpenApiOperation("login")]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = AuthService.Authenticate(request.Username, request.Password);

            if(token.State != LoginState.OK)
            {
                return Unauthorized(new UnauthorizedResponse { State = token.State });
            }

            return Ok(token.Token);
        }
    }
}