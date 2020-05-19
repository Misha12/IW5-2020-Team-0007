using MovieDatabase.Data.Models.Movies;
using System;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Persons
{
    /// <summary>
    /// Detailed information about Person.
    /// </summary>
    public class Person : SimplePerson
    {
        /// <summary>
        /// Datetime of person birth.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Age of person.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Collection with movies, in which the person played.
        /// </summary>
        public List<SimpleMovie> ActingIn { get; set; }

        /// <summary>
        /// Collection with movies, which the person directs.
        /// </summary>
        public List<SimpleMovie> DirectedIn { get; set; }
    }
}
