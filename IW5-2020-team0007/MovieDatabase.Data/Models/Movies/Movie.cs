using MovieDatabase.Data.Models.Persons;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    /// <summary>
    /// Detail information about movie.
    /// </summary>
    public class Movie : SimpleMovie
    {
        /// <summary>
        /// Collection of persons, who play in the film.
        /// </summary>
        public List<SimplePerson> Actors { get; set; }

        /// <summary>
        /// Collection of persons, who direct the film.
        /// </summary>
        public List<SimplePerson> Directors { get; set; }
    }
}
