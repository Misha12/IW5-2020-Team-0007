using MovieDatabase.Data.Models.Movies;
using MovieDatabase.Data.Models.Users;

namespace MovieDatabase.Data.Models.Ratings
{
    public class Rating
    {
        /// <summary>
        /// Unique ID of rate.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Full text of rate.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Score of movie in range 0% to 100%.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Simple information about movie.
        /// </summary>
        public SimpleMovie Movie { get; set; }

        /// <summary>
        /// Author of Rating. If rate is anonymous. Rate have null value.
        /// </summary>
        public SimpleUser Author { get; set; }
    }
}
