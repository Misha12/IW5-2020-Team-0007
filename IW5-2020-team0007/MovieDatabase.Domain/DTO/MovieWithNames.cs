using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Domain.DTO
{
    public class MovieWithNames : Movie
    {
        public List<MovieName> Names { get; set; }

        public MovieWithNames(Entity.Movie movie) : base(movie)
        {
            Names = movie.Names?.Select(o => new MovieName(o)).ToList();
        }
    }
}
