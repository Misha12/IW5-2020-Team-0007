using MovieDatabase.Data.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class CreateMovieRequest
    {
        /// <summary>
        /// Original name of movie.
        /// </summary>
        [Required(ErrorMessage = "The original title of the movie is required.")]
        public string OriginalName { get; set; }

        /// <summary>
        /// Collection of unique IDs of genres.
        /// </summary>
        [GenreIDsExists(ErrorMessage = "Invalid genre list. Some of the genres do not exist.")]
        public List<int> GenreIds { get; set; }

        /// <summary>
        /// Length of movie in seconds. Negative values are not allowed.
        /// </summary>
        [Range(0, long.MaxValue, ErrorMessage = "Invalid movie length. Negative values are not allowed.")]
        public long Length { get; set; }

        /// <summary>
        /// Origin country of movie.
        /// </summary>
        [Required(ErrorMessage = "No country of origin specified.")]
        [StringLength(50, ErrorMessage = "The maximum length of the country of origin is 50 characters.")]
        public string Country { get; set; }

        /// <summary>
        /// Description of movie.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL to title image of URL.
        /// </summary>
        [Url(ErrorMessage = "Invalid profile image address format.")]
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// Year of movie release.
        /// </summary>
        [Required(ErrorMessage = "The year the movie was released is required.")]
        public int? CreatedYear { get; set; }

        /// <summary>
        /// Collection of movie name translations.
        /// </summary>
        public List<MovieNameRequest> MovieNames { get; set; }

        /// <summary>
        /// Collection of person IDs, who play in the film.
        /// </summary>
        [PersonsExists(ErrorMessage = "Someone in the actor list doesn't exist.")]
        public List<long> Actors { get; set; }

        /// <summary>
        /// Collection of person IDs, who direct the film.
        /// </summary>
        [PersonsExists(ErrorMessage = "Some of the people in the list of directors do not exist.")]
        public List<long> Directors { get; set; }
    }
}
