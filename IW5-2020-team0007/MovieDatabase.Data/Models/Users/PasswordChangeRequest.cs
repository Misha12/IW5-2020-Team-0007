using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class PasswordChangeRequest
    {
        /// <summary>
        /// Password. Required minimal 6 characters.
        /// </summary>
        [Required(ErrorMessage = "A password is required.")]
        [MinLength(6, ErrorMessage = "The minimum password length is 6 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirmation of password input. Value have to be same with Password property.
        /// </summary>
        [Required(ErrorMessage = "Password confirmation is required.")]
        [MinLength(6, ErrorMessage = "The minimum password length is 6 characters.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords entered are not the same.")]
        public string PasswordConfirm { get; set; }
    }
}
