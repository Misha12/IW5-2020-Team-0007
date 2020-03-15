using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.API.Models
{
    public class GenreInput
    {
        /// <summary>
        /// Name of genre.
        /// </summary>
        public string Name { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Name);
    }
}
