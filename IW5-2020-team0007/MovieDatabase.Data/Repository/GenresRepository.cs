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
            var genres = Context.Genres
                .Where(o => genreIds.Contains(o.ID))
                .Select(o => o.ID)
                .ToList();

            foreach(var id in genreIds)
            {
                if (!genres.Contains(id))
                    return false;
            }

            return true;
        }
    }
}
