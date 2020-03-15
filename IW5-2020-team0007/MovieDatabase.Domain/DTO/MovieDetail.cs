using MovieDatabase.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Domain.DTO
{
    public class MovieDetail : Movie
    {
        public List<MovieName> Names { get; set; }

        public List<Person> Actors { get; set; }
        public List<Person> Directors { get; set; }
    }
}
