using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MovieDatabase.Data.Entity;
using MovieDatabase.Data.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace MovieDatabase.Data.Repository
{
    public class MoviesRepository : RepositoryBase
    {
        public MoviesRepository(AppDbContext context) : base(context)
        {
        }

        private IQueryable<Movie> GetQuery(bool includeNames = false, bool includeGenres = false, bool includeAllTables = false)
        {
            var query = Context.Movies.AsQueryable();

            if(includeNames || includeAllTables)
            {
                query = query.Include(o => o.Names);
            }

            if(includeGenres || includeAllTables)
            {
                query = query
                    .Include(o => o.Genres)
                    .ThenInclude(o => o.Genre);
            }

            if(includeAllTables)
            {
                query = query
                    .Include(o => o.Persons)
                    .ThenInclude(o => o.Person)
                    .Include(o => o.Ratings)
                    .ThenInclude(o => o.User);
            }

            return query;
        }

        public bool ExistsMovie(long id)
        {
            return GetQuery(false, false).Any(o => o.ID == id);
        }

        public Movie CreateMovie(string originalName, string titleImageUrl, string country, long length, string description,
            int createdYear, Dictionary<string, string> names, List<long> actorIds, List<long> directorIds, List<int> genreIds)
        {
            var entity = new Movie()
            {
                Country = country,
                CreatedYear = createdYear,
                Description = description,
                Length = length,
                Genres = genreIds.Select(o => new GenreMap() { GenreID = o }).ToHashSet(),
                Names = names.Select(o => new MovieName() { Lang = o.Key, Name = o.Value }).ToHashSet(),
                OriginalName = originalName,
                TitleImageUrl = titleImageUrl
            };

            foreach(var actorId in actorIds)
            {
                entity.Persons.Add(new MoviePerson() { PersonID = actorId, Type = MoviePersonType.Actor });
            }

            foreach(var directorId in directorIds)
            {
                entity.Persons.Add(new MoviePerson() { PersonID = directorId, Type = MoviePersonType.Director });
            }

            Context.Movies.Add(entity);
            Context.SaveChanges();

            return GetMovieByID(entity.ID);
        }

        public Movie GetMovieByID(long id)
        {
            return GetQuery(includeAllTables: true)
                .SingleOrDefault(o => o.ID == id);
        }

        public IQueryable<Movie> GetMovies(string name, List<int> genreIds, string country, long? lengthFrom, long? lengthTo)
        {
            var query = GetQuery(true, true);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(o => o.OriginalName.Contains(name) || o.Names.Any(o => o.Name.Contains(name)));

            if (genreIds != null && genreIds.Count > 0)
                query = query.Where(o => o.Genres.Any(x => genreIds.Contains(x.GenreID)));

            if (!string.IsNullOrEmpty(country))
                query = query.Where(o => o.Country == country);

            if (lengthFrom != null)
                query = query.Where(o => o.Length >= lengthFrom.Value);

            if (lengthTo != null)
                query = query.Where(o => o.Length < lengthTo.Value);

            return query;
        }

        public Movie UpdateMovie(long id, string originalName, string titleImageUrl, string country, long? length, string description,
            int? createdYear, Dictionary<string, string> names, List<long> actorIds, List<long> directorIds, List<int> genreIds)
        {
            var movie = GetQuery(includeAllTables: true)
                .SingleOrDefault(o => o.ID == id);

            if (movie == null)
                return null;

            if (!string.IsNullOrEmpty(originalName))
                movie.OriginalName = originalName;

            if (!string.IsNullOrEmpty(titleImageUrl))
                movie.TitleImageUrl = titleImageUrl;

            if (!string.IsNullOrEmpty(country))
                movie.Country = country;

            if (description != null)
                movie.Description = description;

            if (createdYear != null)
                movie.CreatedYear = createdYear.Value;

            if (names != null)
                movie.Names = names.Select(o => new MovieName() { Lang = o.Key, Name = o.Value }).ToHashSet();

            if (length != null)
                movie.Length = length.Value;

            if(actorIds != null)
            {
                foreach(var actor in movie.Persons.Where(o => o.Type == MoviePersonType.Actor).ToList())
                {
                    movie.Persons.Remove(actor);
                }

                foreach (var actorId in actorIds)
                {
                    movie.Persons.Add(new MoviePerson() { PersonID = actorId, Type = MoviePersonType.Actor });
                }
            }

            if(directorIds != null)
            {
                foreach(var director in movie.Persons.Where(o => o.Type == MoviePersonType.Director).ToList())
                {
                    movie.Persons.Remove(director);
                }

                foreach (var directorId in directorIds)
                {
                    movie.Persons.Add(new MoviePerson() { PersonID = directorId, Type = MoviePersonType.Director });
                }
            }

            if (genreIds != null)
                movie.Genres = genreIds.Select(o => new GenreMap() { GenreID = o }).ToHashSet();

            Context.SaveChanges();
            return movie;
        }

        public void DeleteMovie(long id)
        {
            var entity = GetMovieByID(id);

            if (entity == null)
                return;

            Context.Movies.Remove(entity);
            Context.SaveChanges();
        }
    }
}
