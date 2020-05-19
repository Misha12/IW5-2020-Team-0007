using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class MovieNameRequest
    {
        /// <summary>
        /// Language of movie name translation.
        /// </summary>
        [Required(ErrorMessage = "Name translation language is required.")]
        [StringLength(5, ErrorMessage = "The maximum length of the translation language is 5 characters.")]
        public string Lang { get; set; }

        /// <summary>
        /// Translated movie name.
        /// </summary>
        [Required(ErrorMessage = "A translated movie title is required.")]
        [StringLength(255, ErrorMessage = "The maximum length of a translated name is 255 characters.")]
        public string Name { get; set; }
    }
}
