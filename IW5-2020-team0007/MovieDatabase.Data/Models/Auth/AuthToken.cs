using System;

namespace MovieDatabase.Data.Models.Auth
{
    public class AuthToken
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }
}
