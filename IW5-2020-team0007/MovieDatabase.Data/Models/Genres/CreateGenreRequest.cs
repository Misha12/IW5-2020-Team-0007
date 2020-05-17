using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Genres
{
    public class CreateGenreRequest
    {
        /// <summary>
        /// Name of genre. Required unique name.
        /// </summary>
        [Required(ErrorMessage = "Genre name is required.")]
        [GenreNameNotExists]
        public string Name { get; set; }
    }
}
