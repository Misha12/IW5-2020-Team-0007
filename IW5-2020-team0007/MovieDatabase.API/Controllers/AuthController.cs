using System;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthController : Controller
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

            if (token.State != LoginState.OK)
                return Unauthorized(new UnauthorizedResponse { State = token.State });

            return Ok(token.Token);
        }

        [HttpPost("refresh")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation("refreshToken")]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult RefreshToken([FromQuery] string refreshToken)
        {
            var token = AuthService.GetRefreshedToken(refreshToken);

            if (token.State != LoginState.OK)
                return NotFound(new UnauthorizedResponse { State = token.State });

            return Ok(token.Token);
        }

        [HttpDelete("refresh")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation("deleteAllTokens")]
        [ProducesResponseType(typeof(DeleteAllTokensResponse), (int)HttpStatusCode.OK)]
        public IActionResult DeleteAllTokens()
        {
            var currentUserID = Convert.ToInt64(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            return Ok(AuthService.DeleteAllRefreshTokens(currentUserID));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                AuthService.Dispose();

            base.Dispose(disposing);
        }
    }
}