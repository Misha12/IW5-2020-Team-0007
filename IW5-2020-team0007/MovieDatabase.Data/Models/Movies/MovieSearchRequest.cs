using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Validators;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    public class MovieSearchRequest : PaginatedRequest
    {
        public string Name { get; set; }

        /// <summary>
        /// Collection of genre IDs.
        /// </summary>
        [GenreIDsExists(AllowNulls = true, ErrorMessage = "Invalid genre list. Some genres do not exist.")]
        public List<int> GenreIds { get; set; }
        public string Country { get; set; }
        public long? LengthFrom { get; set; }
        public long? LengthTo { get; set; }
    }
}
