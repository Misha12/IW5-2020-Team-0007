using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Auth
{
    public class RefreshTokenRequest
    {
        [Required]
        [MinLength(10, ErrorMessage = "The minimum token length is 10 characters.")]
        public string Token { get; set; }
    }
}
