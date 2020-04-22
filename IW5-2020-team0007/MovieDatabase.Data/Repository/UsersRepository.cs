using MovieDatabase.Data.Entity;
using System;
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

        public IQueryable<User> GetUserList(string usernameQuery)
        {
            var query = Context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(usernameQuery))
                query = query.Where(o => o.Username.Contains(usernameQuery));

            return query;
        }

        public User FindUserByUsername(string username)
        {
            return Context.Users.SingleOrDefault(o => o.Username == username);
        }

        public void AddRefreshToken(User user, string refreshToken)
        {
            user.RefreshTokens.Add(new RefreshToken() { Token = refreshToken });
            Context.SaveChanges();
        }

        public void ClearExpiredTokens(int expirationDays)
        {
            var entities = Context.RefreshTokens
                .Where(o => o.CreatedAt.AddDays(expirationDays) <= DateTime.UtcNow);

            Context.RefreshTokens.RemoveRange(entities);
            Context.SaveChanges();
        }
    }
}
