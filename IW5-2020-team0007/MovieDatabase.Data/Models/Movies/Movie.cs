using MovieDatabase.Data.Models.Persons;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    public class Movie : SimpleMovie
    {
        public List<SimplePerson> Actors { get; set; }
        public List<SimplePerson> Directors { get; set; }
    }
}
