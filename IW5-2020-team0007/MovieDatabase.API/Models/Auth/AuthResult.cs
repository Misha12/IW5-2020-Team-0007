using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Auth;

namespace MovieDatabase.API.Models.Auth
{
    public class AuthResult
    {
        public LoginState State { get; set; }
        public AuthToken Token { get; set; }
    }
}
