using MovieDatabase.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class UserEditRequest
    {
        [EmailAddress(ErrorMessage = "Email není v platném formátu.")]
        public string Email { get; set; }

        [StringLength(255, ErrorMessage = "Povolená délka uživatelského jména je mezi 5 až 255 znaků.", MinimumLength = 5)]
        public string Username { get; set; }
    }
}
