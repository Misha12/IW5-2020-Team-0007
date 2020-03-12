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

            var data = query.ToList();
            return data.Select(Genre.FromEntity).ToList();
        }

        public Genre FindGenreById(int id)
        {
            var item = Context.Genres.FirstOrDefault(o => o.ID == id);

            return item == null ? null : Genre.FromEntity(item);
        }

        public Genre CreateGenre(string name)
        {
            var entity = new DBGenre() { Name = name };

            Context.Add(entity);
            Context.SaveChanges();

            return Genre.FromEntity(entity);
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
            return Genre.FromEntity(item);
        }
    }
}
