using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Domain.DTO
{
    public class PersonDetail : Person
    {
        public List<Movie> ActingIn { get; set; }
        public List<Movie> DirectedIn { get; set; }

        public PersonDetail(Entity.Person person) : base(person)
        {
            if (person.InMovies?.Count > 0)
            {
                ActingIn = person.InMovies.Where(o => o.Type == Entity.MoviePersonType.Actor).Select(o => new Movie(o.Movie)).ToList();
                DirectedIn = person.InMovies.Where(o => o.Type == Entity.MoviePersonType.Director).Select(o => new Movie(o.Movie)).ToList();
            }
        }

        public PersonDetail() { }
    }
}
