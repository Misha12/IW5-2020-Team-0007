using System;

namespace MovieDatabase.Data.Models.Auth
{
    public class AuthToken
    {
        /// <summary>
        /// JWT tokens for using secured methods.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Type of token. Constant Bearer.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// DateTime when token expires.
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Token for generate new access token.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
