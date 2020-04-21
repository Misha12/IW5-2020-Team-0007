using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Auth
{
    public class RefreshTokenRequest
    {
        [Required]
        [MinLength(10)]
        public string Token { get; set; }
    }
}
