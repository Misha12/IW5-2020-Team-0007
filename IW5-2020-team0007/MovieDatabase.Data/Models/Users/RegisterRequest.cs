using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Uživatelské jméno je vyžadováno.")]
        [StringLength(255, ErrorMessage = "Povolená délka uživatelského jména je mezi 5 až 255 znaků.", MinimumLength = 5)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Heslo je vyžadováno")]
        [MinLength(6, ErrorMessage = "Minimální délka hesla je 6 znaků.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrzení hesla je požadováno.")]
        [MinLength(6, ErrorMessage = "Minimální délka hesla je 6 znaků.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Zadaná hesla nejsou stejná.")]
        public string PasswordConfirm { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email je vyžadován.")]
        public string Email { get; set; }
    }
}
