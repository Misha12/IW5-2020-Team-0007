using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class PasswordChangeRequest
    {
        [Required(ErrorMessage = "Heslo je vyžadováno")]
        [MinLength(6, ErrorMessage = "Minimální délka hesla je 6 znaků.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potvrzení hesla je požadováno.")]
        [MinLength(6, ErrorMessage = "Minimální délka hesla je 6 znaků.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Zadaná hesla nejsou stejná.")]
        public string PasswordConfirm { get; set; }
    }
}
