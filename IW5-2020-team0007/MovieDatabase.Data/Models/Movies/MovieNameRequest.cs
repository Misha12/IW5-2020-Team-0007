using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class MovieNameRequest
    {
        /// <summary>
        /// Language of movie name translation.
        /// </summary>
        [Required(ErrorMessage = "Name translation language is required.")]
        public string Lang { get; set; }

        /// <summary>
        /// Translated movie name.
        /// </summary>
        [Required(ErrorMessage = "A translated movie title is required.")]
        public string Name { get; set; }
    }
}
