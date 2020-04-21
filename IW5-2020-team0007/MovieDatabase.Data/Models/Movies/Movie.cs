using MovieDatabase.Data.Models.Person;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    public class Movie : SimpleMovie
    {
        public List<MovieName> TranslatedNames { get; set; }
        public List<SimplePerson> Actors { get; set; }
        public List<SimplePerson> Directors { get; set; }
    }
}
