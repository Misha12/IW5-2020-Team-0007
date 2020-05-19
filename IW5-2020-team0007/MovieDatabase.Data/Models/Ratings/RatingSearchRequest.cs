using MovieDatabase.Data.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Ratings
{
    public class RatingSearchRequest : PaginatedRequest
    {
        /// <summary>
        /// Collection of unique IDs of movies.
        /// </summary>
        public List<long> MovieIDs { get; set; }

        /// <summary>
        /// Text must contains.
        /// </summary>
        public string TextContains { get; set; }

        /// <summary>
        /// Minimal score of rate. Value is in range 0% to 100%.
        /// </summary>
        [Range(0, 100, ErrorMessage = "The allowed range of numerical evaluation is between 0 and 100 points.")]
        public int? ScoreFrom { get; set; }

        /// <summary>
        /// Maximal score of rate. Value is in range 0% to 100%.
        /// </summary>
        [Range(0, 100, ErrorMessage = "The allowed range of numerical evaluation is between 0 and 100 points.")]
        public int? ScoreTo { get; set; }
    }
}
