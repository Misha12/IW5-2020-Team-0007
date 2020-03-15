using Microsoft.EntityFrameworkCore;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DBGenre = MovieDatabase.Domain.Entity.Genre;

namespace MovieDatabase.API.Services
{
    public class GenreService
    {
        private MovieDatabaseContext Context { get; }
        private IMapper Mapper { get; }

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
            return Mapper.Map<List<Genre>>(data);
        }

        public GenreDetail FindGenreByID(int id)
        {
            var item = Context.Genres
                .Include(o => o.Movies)
                .FirstOrDefault(o => o.ID == id);
            var mapped = Mapper.Map<GenreDetail>(item);

            if (item.Movies?.Count > 0)
            {
                mapped.Movies = item.Movies.Where(o => o.GenreID == item.ID).Select(o => Mapper.Map<Movie>(o)).ToList();
            }

            return mapped;

        }

        public Genre CreateGenre(string name)
        {
            var entity = new DBGenre() { Name = name };

            Context.Add(entity);
            Context.SaveChanges();

            return Mapper.Map<Genre>(entity);
        }

        public bool DeleteGenre(int id)
        {
            var item = Context.Genres
                .Include(o => o.Movies)
                .FirstOrDefault(o => o.ID == id);

            if (item == null)
                return false;

            if(item.Movies?.Count > 0)
            {
                foreach(var movie in item.Movies)
                {
                    movie.GenreID = 0;
                }
            }

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
            return Mapper.Map<GenreDetail>(item);
        }
    }
}
