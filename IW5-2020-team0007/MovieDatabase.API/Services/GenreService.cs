using Microsoft.EntityFrameworkCore;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using DBGenre = MovieDatabase.Domain.Entity.Genre;

namespace MovieDatabase.API.Services
{
    public class GenreService
    {
        private MovieDatabaseContext Context { get; }

        public GenreService(MovieDatabaseContext context)
        {
            Context = context;
        }

        public List<Genre> GetFullList(string search = null)
        {
            var query = Context.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(o => o.Name.Contains(search));

            var data = query.ToList();
            return data.Select(o => new Genre(o)).ToList();
        }

        public GenreDetail FindGenreByID(int id)
        {
            var item = Context.Genres
                .Include(o => o.Movies)
                .FirstOrDefault(o => o.ID == id);

            return item == null ? null : new GenreDetail(item);
        }

        public Genre CreateGenre(string name)
        {
            var entity = new DBGenre() { Name = name };

            Context.Add(entity);
            Context.SaveChanges();

            return new Genre(entity);
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

        public GenreDetail UpdateGenre(int id, string newName)
        {
            var item = Context.Genres
                .Include(o => o.Movies)
                .FirstOrDefault(o => o.ID == id);

            if (item == null)
                return null;

            item.Name = newName;

            Context.SaveChanges();
            return new GenreDetail(item);
        }
    }
}
