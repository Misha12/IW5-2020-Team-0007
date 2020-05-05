using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class ConfirmRegisterRequest
    {
        [Required(ErrorMessage = "Autorizační kód je vyžadován.")]
        [AuthorizationCodeExists]
        public string Code { get; set; }
    }
}
