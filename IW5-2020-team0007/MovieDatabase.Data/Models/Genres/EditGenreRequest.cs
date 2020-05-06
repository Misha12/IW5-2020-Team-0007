using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Genres
{
    public class EditGenreRequest
    {
        [Required(ErrorMessage = "Nový název žánru je povinný.")]
        [UsernameNotExists]
        public string Name { get; set; }
    }
}
