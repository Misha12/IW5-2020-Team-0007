using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MovieDatabase.Data.Entity;
using MovieDatabase.Data.Enums;
using System;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class UsersRepository : RepositoryBase
    {
        public UsersRepository(AppDbContext context) : base(context)
        {
        }

        private IQueryable<User> GetQuery(bool includeTables)
        {
            var query = Context.Users.AsQueryable();

            if (includeTables)
            {
                query = query
                    .Include(o => o.Ratings)
                    .Include(o => o.RefreshTokens);
            }

            return query;
        }

        public bool UserIDExists(long id)
        {
            return GetQuery(false).Any(o => o.ID == id);
        }

        public bool UsernameExists(string username)
        {
            return GetQuery(false).Any(o => o.Username == username);
        }

        public IQueryable<User> GetUserList(string usernameQuery)
        {
            var query = GetQuery(false);

            if (!string.IsNullOrEmpty(usernameQuery))
                query = query.Where(o => o.Username.Contains(usernameQuery));

            return query;
        }

        public User FindUserByUsername(string username)
        {
            return GetQuery(false)
                .SingleOrDefault(o => o.Username == username);
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

        public User CreateRegisteredUser(string username, string password, string email, string authCode)
        {
            var entity = new User()
            {
                Username = username,
                Email = email,
                Password = password,
                RegisteredAt = DateTime.Now,
                Role = Roles.Registered,
                AuthCode = authCode
            };

            Context.Users.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public bool AuthCodeExists(string code)
        {
            return GetQuery(false).Any(o => o.AuthCode == code);
        }

        public User ConfirmUserRegistration(string code)
        {
            var user = GetQuery(false).SingleOrDefault(o => o.AuthCode == code);

            user.Role = Roles.User;
            user.AuthCode = null;

            Context.SaveChanges();
            return user;
        }

        public User FindUserById(long id)
        {
            return GetQuery(true)
                .FirstOrDefault(o => o.ID == id);
        }

        public User UpdateUser(long id, string newEmail, string newUsername)
        {
            var user = FindUserById(id);

            if (user == null)
                return null;

            if (!string.IsNullOrEmpty(newEmail))
                user.Email = newEmail;

            if (!string.IsNullOrEmpty(newUsername))
                user.Username = newUsername;

            Context.SaveChanges();
            return user;
        }

        public User ChangePassword(long id, string newPassword)
        {
            var user = FindUserById(id);

            if (user == null)
                return null;

            user.Password = newPassword;

            Context.SaveChanges();
            return user;
        }

        public void DeleteUser(long id)
        {
            var user = FindUserById(id);

            user.RefreshTokens.Clear();
            user.Ratings.Clear();

            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        public RefreshToken FindRefreshToken(string token)
        {
            return Context.RefreshTokens
                .Include(o => o.User)
                .SingleOrDefault(o => o.Token == token);
        }

        public int? DeleteAllRefreshTokens(long userID)
        {
            var user = FindUserById(userID);

            if (user == null)
                return null;

            var count = user.RefreshTokens.Count;
            user.RefreshTokens.Clear();
            Context.SaveChanges();
            
            return count;
        }

        public User ChangeUserRole(long id, Roles newRole)
        {
            var user = FindUserById(id);

            if (user == null)
                return null;

            if (newRole > Roles.Registered)
                user.Role = newRole;

            Context.SaveChanges();
            return user;
        }
    }
}
