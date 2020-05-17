using MovieDatabase.Data.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Ratings
{
    public class EditRatingRequest
    {
        [MovieIDExists(AllowNull = true, ErrorMessage = "Film s požadovaným ID neexistuje.")]
        public long? NewMovieID { get; set; }

        [MinLength(10, ErrorMessage = "Minimální délka textového hodnocení je 10 znaků.")]
        public string Text { get; set; }

        [Range(0, 100, ErrorMessage = "Povolený rozsah číselného hodnocení je mezi 0 až 100 body.")]
        public int? Score { get; set; }

        public bool? Anonymous { get; set; }
    }
}
