using MovieDatabase.Data.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Ratings
{
    public class CreateRateRequest
    {
        [Required(ErrorMessage = "Identifikace filmu je požadována.")]
        [MovieIDExists(ErrorMessage = "Film s požadovaným ID neexistuje.")]
        public long MovieID { get; set; }

        [Required(ErrorMessage = "Textové hodnocení je povinné.")]
        [MinLength(10, ErrorMessage = "Minimální délka textového hodnocení je 10 znaků.")]
        public string Text { get; set; }

        [Range(0, 100, ErrorMessage = "Povolený rozsah číselného hodnocení je mezi 0 až 100 body.")]
        public int Score { get; set; }

        // Null je anonymní.
        [UserIDExists(AllowNull = true, ErrorMessage = "Uživatel se zadaným ID neexistuje.")]
        public long? UserID { get; set; }
    }
}
