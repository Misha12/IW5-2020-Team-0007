using MovieDatabase.Data.Validators;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Genres
{
    public class EditGenreRequest
    {
        /// <summary>
        /// Name of genre. Required unique name.
        /// </summary>
        [Required(ErrorMessage = "New genre name is required.")]
        [UsernameNotExists]
        public string Name { get; set; }
    }
}
