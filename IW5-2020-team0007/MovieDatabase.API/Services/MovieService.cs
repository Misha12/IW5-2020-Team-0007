using Microsoft.EntityFrameworkCore;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using MovieEntity = MovieDatabase.Domain.Entity.Movie;

namespace MovieDatabase.API.Services
{
    public class MovieService
    {
        private MovieDatabaseContext Context { get; }

        public MovieService(MovieDatabaseContext context)
        {
            Context = context;
        }

        public List<Movie> GetMovieList(string searchName, int[] genres, long? lengthFrom, long? lengthTo, string[] countries)
        {
            var query = Context.Movies.Include(o => o.Genre).AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
                query = query.Where(o => o.OriginalName.Contains(searchName));

            if (genres?.Length > 0)
                query = query.Where(o => genres.Contains(o.GenreID));

            if (lengthFrom != null)
                query = query.Where(o => o.Length >= lengthFrom.Value);

            if (lengthTo != null)
                query = query.Where(o => o.Length < lengthTo.Value);

            if (countries?.Length > 0)
                query = query.Where(o => countries.Contains(o.Country));

            var data = query.ToList();
            return data.Select(Movie.FromEntity).ToList();
        }

        public Movie FindMovieById(long id)
        {
            var item = Context.Movies.Include(o => o.Genre).FirstOrDefault(o => o.ID == id);
            return item == null ? null : Movie.FromEntity(item);
        }

        public Movie CreateMovie(string originalName, int genreID, long length, string country, string description = null)
        {
            var entity = new MovieEntity()
            {
                Country = country,
                Description = description,
                GenreID = genreID,
                Length = length,
                OriginalName = originalName
            };

            Context.Movies.Add(entity);
            Context.SaveChanges();

            return Movie.FromEntity(entity);
        }

        public bool DeleteMovie(long id)
        {
            var item = Context.Movies.FirstOrDefault(o => o.ID == id);

            if (item == null)
                return false;

            Context.Movies.Remove(item);
            Context.SaveChanges();

            return true;
        }

        public Movie UpdateMovie(long id, string originalName = null, int? genre = null, long? length = null, string country = null, string description = null)
        {
            var item = Context.Movies.FirstOrDefault(o => o.ID == id);

            if (item == null)
                return null;

            if (!string.IsNullOrEmpty(originalName))
                item.OriginalName = originalName;

            if (genre != null)
                item.GenreID = genre.Value;

            if (length != null)
                item.Length = length.Value;

            if (!string.IsNullOrEmpty(country))
                item.Country = country;

            if (!string.IsNullOrEmpty(description))
                item.Description = description;

            Context.SaveChanges();
            return FindMovieById(id);
        }
    }
}
