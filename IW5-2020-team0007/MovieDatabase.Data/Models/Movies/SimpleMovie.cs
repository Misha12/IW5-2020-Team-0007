using MovieDatabase.Data.Models.Genres;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    /// <summary>
    /// Simple information about movie.
    /// </summary>
    public class SimpleMovie
    {
        /// <summary>
        /// Unique ID of movie.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Original name of movie.
        /// </summary>
        public string OriginalName { get; set; }

        /// <summary>
        /// URL to title image of movie.
        /// </summary>
        public string TitleImageUrl { get; set; }

        /// <summary>
        /// Origin country of movie.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Length of movie in seconds.
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// Collection of genres associated with movie.
        /// </summary>
        public List<SimpleGenre> Genres { get; set; }

        /// <summary>
        /// Year of publication movie.
        /// </summary>
        public int CreatedYear { get; set; }

        /// <summary>
        /// Collection of movie translations.
        /// </summary>
        public List<MovieName> TranslatedNames { get; set; }
    }
}
