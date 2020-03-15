using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Domain.DTO
{
    public class GenreDetail : Genre
    {
        public List<Movie> Movies { get; set; }

        public GenreDetail(Entity.Genre genre) : base(genre)
        {
            if(genre.Movies?.Count > 0)
            {
                Movies = genre.Movies.Select(o => new Movie(o)).ToList();
            }
        }

        public GenreDetail() { }
    }
}
