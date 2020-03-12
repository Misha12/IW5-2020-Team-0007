using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.API.Models
{
    public class GenreInput
    {
        /// <summary>
        /// Název žánru.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
