using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.API.Models
{
    public class GenreInput
    {
        /// <summary>
        /// Název žánru.
        /// </summary>
        public string Name { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Name);
    }
}
