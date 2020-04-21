using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class ConfirmRegisterRequest
    {
        [Required(ErrorMessage = "Autorizační kód je vyžadován.")]
        [StringLength(50, ErrorMessage = "Neplatný formát registračního kódu.", MinimumLength = 10)]
        public string Code { get; set; }
    }
}
