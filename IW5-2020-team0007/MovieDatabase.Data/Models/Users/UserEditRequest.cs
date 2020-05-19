using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class UserEditRequest
    {
        /// <summary>
        /// New email of user. Required valid email format.
        /// </summary>
        [EmailAddress(ErrorMessage = "Email is not in valid format.")]
        public string Email { get; set; }

        /// <summary>
        /// New username of user. Required unique name.
        /// </summary>
        [StringLength(255, ErrorMessage = "The allowed length of the user name is between 5 and 255 characters.", MinimumLength = 5)]
        public string Username { get; set; }
    }
}
