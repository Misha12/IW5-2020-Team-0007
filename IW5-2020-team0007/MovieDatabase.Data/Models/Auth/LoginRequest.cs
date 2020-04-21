using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Auth
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
