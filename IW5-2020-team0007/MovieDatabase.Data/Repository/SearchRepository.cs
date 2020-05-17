using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data.Entity;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class SearchRepository : RepositoryBase
    {
        public SearchRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Movie> SearchInMovies(string keyword)
        {
            var query = Context.Movies
                .Include(o => o.Names)
                .AsNoTracking();

            return query.Where(o =>o.OriginalName.Contains(keyword) || o.Names.Any(x => x.Name.Contains(keyword)));
        }

        public IQueryable<Person> SearchInPersons(string keyword)
        {
            var query = Context.Persons.AsNoTracking();
            return query.Where(o => (o.Name + " " + o.Surname).Contains(keyword));
        }

        public IQueryable<User> SearchInUsers(string keyword)
        {
            var query = Context.Users.AsNoTracking();
            return query.Where(o => o.Username.Contains(keyword));
        }

        public IQueryable<Rating> SearchInRatings(string keyword)
        {
            var query = Context.Rates
                .Include(o => o.User)
                .Include(o => o.Movie)
                .ThenInclude(o => o.Genres)
                .ThenInclude(o => o.Genre)
                .Include(o => o.Movie)
                .ThenInclude(o => o.Names)
                .AsNoTracking();

            return query.Where(o => o.Text.Contains(keyword));
        }
    }
}
