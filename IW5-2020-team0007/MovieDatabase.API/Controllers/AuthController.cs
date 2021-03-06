﻿using System;
using System.ComponentModel;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<AuthController> Logger { get; }

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            AuthService = authService;
            Logger = logger;
        }

        /// <summary>
        /// Authorize user and gets JWT token.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        [OpenApiOperation(nameof(AuthController) + "_" + nameof(Login))]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), (int)HttpStatusCode.Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = AuthService.Authenticate(request.Username, request.Password);

            if (token.State != LoginState.OK)
            {
                Logger.LogWarning("Executed invalid login of user {0}. State: {1}", request.Username, token.State.ToString());
                return Unauthorized(new UnauthorizedResponse { State = token.State });
            }

            Logger.LogInformation("Executed successfull login of user {0}.", request.Username);
            return Ok(token.Token);
        }

        /// <summary>
        /// Creates a new token from refresh token.
        /// </summary>
        [HttpPost("refresh")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(AuthController) + "_" + nameof(RefreshToken))]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), (int)HttpStatusCode.NotFound)]
        public IActionResult RefreshToken([FromQuery] string refreshToken)
        {
            var token = AuthService.GetRefreshedToken(refreshToken);

            if (token.State != LoginState.OK)
            {
                Logger.LogWarning("Executed invalid token refresh request. ({0})", refreshToken);
                return NotFound(new UnauthorizedResponse { State = token.State });
            }

            Logger.LogInformation("Successfull executed token refresh ({0})", refreshToken);
            return Ok(token.Token);
        }

        /// <summary>
        /// Revoke all tokens for current logged user.
        /// </summary>
        [HttpDelete("refresh")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(AuthController) + "_" + nameof(DeleteAllTokens))]
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