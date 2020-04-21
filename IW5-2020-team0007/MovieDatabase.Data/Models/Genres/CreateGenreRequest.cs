using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Genres
{
    public class CreateGenreRequest
    {
        [Required(ErrorMessage = "Název žánru je povinný.")]
        public string Name { get; set; }
    }
}
