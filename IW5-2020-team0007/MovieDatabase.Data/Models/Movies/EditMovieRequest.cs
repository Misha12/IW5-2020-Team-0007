using MovieDatabase.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class EditMovieRequest
    {
        /// <summary>
        /// New original name of movie.
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// New collection of genres associated with movie.
        /// </summary>
        [GenreIDsExists(AllowNulls = true, ErrorMessage = "Invalid genre list. Some of the genres do not exist.")]
        public List<int> GenreIds { get; set; }

        /// <summary>
        /// New length of movie.
        /// </summary>
        [Range(0, long.MaxValue, ErrorMessage = "Invalid movie length. Negative values are not allowed.")]
        public long? Length { get; set; }

        /// <summary>
        /// New origin country of movie.
        /// </summary>
        [StringLength(50, ErrorMessage = "The maximum length of the country of origin is 50 characters.")]
        public string Country { get; set; }

        /// <summary>
        /// New description of movie.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL to title image of movie.
        /// </summary>
        [Url(ErrorMessage = "Invalid profile image address format.")]
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// New year of movie release.
        /// </summary>
        public int? CreatedYear { get; set; }

        /// <summary>
        /// New movie name translations.
        /// </summary>
        public List<MovieNameRequest> MovieNames { get; set; }

        /// <summary>
        /// New collection of unique person IDs, who play in the film.
        /// </summary>
        [PersonsExists(AllowNulls = true, ErrorMessage = "Someone in the actor list doesn't exist.")]
        public List<long> Actors { get; set; }

        /// <summary>
        /// New collection of unique person IDs, who direct the film.
        /// </summary>
        [PersonsExists(AllowNulls = true, ErrorMessage = "Some of the people in the list of directors do not exist.")]
        public List<long> Directors { get; set; }
    }
}
