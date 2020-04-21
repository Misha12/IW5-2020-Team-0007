using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class UsersRepository : RepositoryBase
    {
        public UsersRepository(AppDbContext context) : base(context)
        {
        }

        public bool UserIDExists(long id)
        {
            return Context.Users.Any(o => o.ID == id);
        }
    }
}
