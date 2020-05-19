using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Movies;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Search
{
    public class MovieSearchResult
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
        /// Collection of movie translates.
        /// </summary>
        public List<MovieName> Names { get; set; }

        /// <summary>
        /// Origin country of movie.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Length of movie in seconds.
        /// </summary>
        public long Length { get; set; }
    }
}
