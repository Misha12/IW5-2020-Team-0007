using MovieDatabase.API.Models.Auth;
using UserEntity = MovieDatabase.Data.Entity.User;
using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Auth;
using MovieDatabase.Data.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MovieDatabase.API.Services
{
    public class AuthService : IDisposable
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
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(AuthSettings.ExpirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = AuthSettings.Issuer
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

        public AuthResult GetRefreshedToken(string refreshToken)
        {
            UsersRepository.ClearExpiredTokens(AuthSettings.ExpirationDays);

            var token = UsersRepository.FindRefreshToken(refreshToken);

            if (token == null)
                return new AuthResult() { State = LoginState.RefreshTokenNotFound };

            var jwt = GenerateJwt(token.User);

            return new AuthResult()
            {
                State = LoginState.OK,
                Token = new AuthToken()
                {
                    AccessToken = jwt,
                    ExpiresAt = DateTime.UtcNow.AddDays(AuthSettings.ExpirationDays),
                    Type = "Bearer",
                    RefreshToken = refreshToken
                }
            };
        }

        public DeleteAllTokensResponse DeleteAllRefreshTokens(long userID)
        {
            var count = UsersRepository.DeleteAllRefreshTokens(userID);

            if (count == null)
                return null;

            return new DeleteAllTokensResponse()
            {
                DeletedTokensCount = count.Value
            };
        }

        public void Dispose()
        {
            UsersRepository.Dispose();
        }
    }
}
