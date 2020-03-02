using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.API.Models
{
    public class GenreInput
    {
        [Required]
        public string Name { get; set; }
    }
}
