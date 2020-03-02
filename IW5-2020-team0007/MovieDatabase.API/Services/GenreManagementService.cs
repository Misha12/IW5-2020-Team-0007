using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using DBGenre = MovieDatabase.Domain.Entity.Genre;

namespace MovieDatabase.API.Services
{
    public class GenreManagementService
    {
        private MovieDatabaseContext Context { get; }

        public GenreManagementService(MovieDatabaseContext context)
        {
            Context = context;
        }

        public List<Genre> GetFullList(string search = null)
        {
            var query = Context.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.Name.Contains(search));
            }

            return query
                .Select(o => new Genre()
                {
                    Name = o.Name,
                    ID = o.ID
                })
                .ToList();
        }

        public Genre FindGenreById(int id)
        {
            var item = Context.Genres.FirstOrDefault(o => o.ID == id);

            if (item == null)
                return null;

            return new Genre()
            {
                ID = item.ID,
                Name = item.Name
            };
        }

        public Genre CreateGenre(string name)
        {
            var entity = new DBGenre()
            {
                Name = name
            };

            Context.Add(entity);
            Context.SaveChanges();

            return new Genre()
            {
                Name = entity.Name,
                ID = entity.ID
            };
        }

        public bool DeleteGenre(int id)
        {
            var item = Context.Genres.FirstOrDefault(o => o.ID == id);

            if (item == null)
                return false;

            Context.Genres.Remove(item);
            Context.SaveChanges();

            return true;
        }

        public Genre UpdateGenre(int id, string newName)
        {
            var item = Context.Genres.FirstOrDefault(o => o.ID == id);

            if (item == null)
                return null;

            item.Name = newName;

            Context.SaveChanges();

            return new Genre()
            {
                Name = item.Name,
                ID = item.ID
            };
        }
    }
}
