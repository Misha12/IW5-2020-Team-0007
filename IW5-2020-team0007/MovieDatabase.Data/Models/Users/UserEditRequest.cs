using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class UserEditRequest
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email je vyžadován.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Uživatelské jméno je vyžadováno.")]
        [StringLength(255, ErrorMessage = "Povolená délka uživatelského jména je mezi 5 až 255 znaků.", MinimumLength = 5)]
        public string Username { get; set; }
    }
}
