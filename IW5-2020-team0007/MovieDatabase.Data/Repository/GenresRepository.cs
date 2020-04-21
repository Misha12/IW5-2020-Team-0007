using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class GenresRepository : RepositoryBase
    {
        public GenresRepository(AppDbContext context) : base(context)
        {
        }

        public bool AllGenresExists(List<int> genreIds)
        {
            return Context.Genres.All(o => genreIds.Contains(o.ID));
        }
    }
}
