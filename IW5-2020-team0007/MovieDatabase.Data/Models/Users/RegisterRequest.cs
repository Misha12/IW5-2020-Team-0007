using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    /// <summary>
    /// Registration request.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Unique username.
        /// </summary>
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(255, ErrorMessage = "The allowed length of the user name is between 5 and 255 characters.", MinimumLength = 5)]
        [UsernameNotExists]
        public string Username { get; set; }

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

        /// <summary>
        /// Email. Required valid email format.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not in valid format.")]
        public string Email { get; set; }
    }
}
