namespace MovieDatabase.API.Models.Auth
{
    public class AuthSettings
    {
        public int ExpirationDays { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
    }
}
