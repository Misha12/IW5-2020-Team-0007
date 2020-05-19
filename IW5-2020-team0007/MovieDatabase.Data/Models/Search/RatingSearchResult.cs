using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Movies;
using MovieDatabase.Data.Models.Users;

namespace MovieDatabase.Data.Models.Search
{
    public class RatingSearchResult
    {
        /// <summary>
        /// Short text of rating. Text is cuted to 150 characters.
        /// </summary>
        public string ShortText { get; set; }

        /// <summary>
        /// Score 0% to 100%.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Simple information about movie.
        /// </summary>
        public SimpleMovie Movie { get; set; }

        /// <summary>
        /// Simple information about user. If rating is anonymous, User property have null value.
        /// </summary>
        public SimpleUser User { get; set; }
    }
}
