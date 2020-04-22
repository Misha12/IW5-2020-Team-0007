using MovieDatabase.API.Models.Auth;
using UserEntity = MovieDatabase.Data.Entity.User;
using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Auth;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using MovieDatabase.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MovieDatabase.API.Services
{
    public class AuthService
    {
        private UsersRepository UsersRepository { get; }
        private AuthSettings AuthSettings { get; }

        public AuthService(UsersRepository usersRepository, IOptions<AuthSettings> options)
        {
            UsersRepository = usersRepository;
            AuthSettings = options.Value;
        }

        public AuthResult Authenticate(string username, string password)
        {
            UsersRepository.ClearExpiredTokens(AuthSettings.ExpirationDays);
            
            var user = UsersRepository.FindUserByUsername(username);

            if (user == null)
                return new AuthResult() { State = LoginState.UserNotFound };

            if (!IsValidPassword(user, password))
                return new AuthResult { State = LoginState.InvalidPassword };

            if (user.Role == Roles.Registered)
                return new AuthResult() { State = LoginState.Unverified };

            var jwt = GenerateJwt(user);

            return new AuthResult()
            {
                State = LoginState.OK,
                Token = new AuthToken()
                {
                    ExpiresAt = DateTime.UtcNow.AddDays(AuthSettings.ExpirationDays),
                    Type = "Bearer",
                    AccessToken = jwt,
                    RefreshToken = SetRefreshToken(user, jwt)
                }
            };
        }

        private bool IsValidPassword(UserEntity user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }

        private string GenerateJwt(UserEntity user)
        {
            var key = Encoding.UTF8.GetBytes(AuthSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.ID.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(AuthSettings.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string SetRefreshToken(UserEntity user, string jwt)
        {
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(jwt));
            UsersRepository.AddRefreshToken(user, base64);

            return base64;
        }
    }
}
