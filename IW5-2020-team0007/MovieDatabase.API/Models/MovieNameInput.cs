using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.API.Models
{
    public class MovieNameInput
    {
        [Required]
        public string Lang { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
