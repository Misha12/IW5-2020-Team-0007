using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MovieDatabase.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.Data.Repository
{
    public class GenresRepository : RepositoryBase
    {
        public GenresRepository(AppDbContext context) : base(context)
        {
        }

        private IQueryable<Genre> GetQuery(bool includeMapping)
        {
            var query = Context.Genres.AsQueryable();

            if (includeMapping)
            {
                query = query
                    .Include(o => o.Movies)
                    .ThenInclude(o => o.Movie);
            }

            return query;
        }

        public bool AllGenresExists(List<int> genreIds)
        {
            var genres = Context.Genres
                .Where(o => genreIds.Contains(o.ID))
                .Select(o => o.ID)
                .ToList();

            foreach (var id in genreIds)
            {
                if (!genres.Contains(id))
                    return false;
            }

            return true;
        }

        public IQueryable<Genre> GetGenreList(string searchQuery)
        {
            var query = GetQuery(true);

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(o => o.Name.Contains(searchQuery));

            return query;
        }

        public bool GenreNameExists(string name)
        {
            return GetQuery(false).Any(o => o.Name == name);
        }

        public bool GenreExists(int id)
        {
            return GetQuery(false).Any(o => o.ID == id);
        }

        public Genre CreateGenre(string name)
        {
            var entity = new Genre()
            {
                Name = name
            };

            Context.Genres.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public Genre UpdateGenre(int id, string newName)
        {
            var entity = GetQuery(true)
                .SingleOrDefault(o => o.ID == id);

            if (entity == null)
                return null;

            entity.Name = newName;
            Context.SaveChanges();

            return entity;
        }

        public void DeleteGenre(int id)
        {
            var entity = GetQuery(true)
                .SingleOrDefault(o => o.ID == id);

            if (entity.Movies.Count > 0)
                entity.Movies.Clear();

            Context.Genres.Remove(entity);
            Context.SaveChanges();
        }
    }
}
