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

        public MovieDetail(Entity.Movie movie) : base(movie)
        {
            Names = movie.Names?.Select(o => new MovieName(o)).ToList();

            if (movie.Persons?.Count > 0)
            {
                Actors = movie.Persons.Where(o => o.Type == MoviePersonType.Actor).Select(o => new Person(o.Person)).ToList();
                Directors = movie.Persons.Where(o => o.Type == MoviePersonType.Director).Select(o => new Person(o.Person)).ToList();
            }
        }
    }
}
