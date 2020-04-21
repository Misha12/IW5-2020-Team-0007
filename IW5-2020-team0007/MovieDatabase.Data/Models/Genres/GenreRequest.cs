using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Genres
{
    public class GenreRequest
    {
        [Required(ErrorMessage = "Název žánru je povinný.")]
        public string Name { get; set; }
    }
}
