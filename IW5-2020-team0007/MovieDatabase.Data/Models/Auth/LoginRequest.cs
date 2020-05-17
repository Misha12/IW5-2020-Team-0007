using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        public string Password { get; set; }
    }
}
