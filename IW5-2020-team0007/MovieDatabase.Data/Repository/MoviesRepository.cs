using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class MoviesRepository : RepositoryBase
    {
        public MoviesRepository(AppDbContext context) : base(context)
        {
        }

        public bool ExistsMovie(long id)
        {
            return Context.Movies.Any(o => o.ID == id);
        }
    }
}
