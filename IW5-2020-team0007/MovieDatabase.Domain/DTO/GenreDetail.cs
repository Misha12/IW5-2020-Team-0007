using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Domain.DTO
{
    public class GenreDetail : Genre
    {
        public List<Movie> Movies { get; set; }
    }
}
