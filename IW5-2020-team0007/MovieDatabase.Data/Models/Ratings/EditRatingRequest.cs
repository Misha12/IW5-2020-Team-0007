using MovieDatabase.Data.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Ratings
{
    public class EditRatingRequest
    {
        /// <summary>
        /// Unique ID of movie in case of move rate to another movie.
        /// </summary>
        [MovieIDExists(AllowNull = true, ErrorMessage = "The movie with the requested ID does not exist.")]
        public long? NewMovieID { get; set; }

        /// <summary>
        /// Text in rate. Minimal length of rate is 10 characters.
        /// </summary>
        [MinLength(10, ErrorMessage = "The minimum length of a text rating is 10 characters.")]
        public string Text { get; set; }

        /// <summary>
        /// Score of rate. Values in range 0% to 100%.
        /// </summary>
        [Range(0, 100, ErrorMessage = "The allowed range of numerical evaluation is between 0 and 100 points.")]
        public int? Score { get; set; }

        /// <summary>
        /// Anonymization of the author.
        /// </summary>
        public bool? Anonymous { get; set; }
    }
}
