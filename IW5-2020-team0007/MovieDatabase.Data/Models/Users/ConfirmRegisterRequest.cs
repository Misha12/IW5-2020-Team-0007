using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class ConfirmRegisterRequest
    {
        /// <summary>
        /// Authorization code to finish user registration.
        /// </summary>
        [Required(ErrorMessage = "An authorization code is required.")]
        [AuthorizationCodeExists]
        public string Code { get; set; }
    }
}
