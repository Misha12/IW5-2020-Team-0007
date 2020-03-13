using Microsoft.EntityFrameworkCore;
using MovieDatabase.API.Models;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using MovieEntity = MovieDatabase.Domain.Entity.Movie;
using MoviePersonEntity = MovieDatabase.Domain.Entity.MoviePerson;
using MoviePersonType = MovieDatabase.Domain.Entity.MoviePersonType;

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
            return data.Select(o => new Movie(o)).ToList();
        }

        public Movie FindMovieByID(long id)
        {
            var item = Context.Movies
                .Include(o => o.Genre)
                .Include(o => o.Names)
                .Include(o => o.Persons)
                .ThenInclude(o => o.Person)
                .FirstOrDefault(o => o.ID == id);

            return item == null ? null : new MovieDetail(item);
        }

        public Movie CreateMovie(MovieInput data)
        {
            var entity = new MovieEntity()
            {
                Country = data.Country,
                Description = data.Description,
                GenreID = data.Genre,
                Length = data.Length,
                OriginalName = data.OriginalName,
                TitleImage = data.TitleImageUrl
            };

            if (data.Names?.Count > 0)
                entity.Names = data.Names.Select(o => new Domain.Entity.MovieName() { Lang = o.Lang, Name = o.Name }).ToHashSet();

            if (data.Actors?.Count > 0)
            {
                foreach (var actor in data.Actors)
                {
                    entity.Persons.Add(new MoviePersonEntity() { Type = MoviePersonType.Actor, PersonID = actor });
                }
            }

            if (data.Directors?.Count > 0)
            {
                foreach (var director in data.Directors)
                {
                    entity.Persons.Add(new MoviePersonEntity() { PersonID = director, Type = MoviePersonType.Director });
                }
            }

            Context.Movies.Add(entity);
            Context.SaveChanges();

            return new MovieDetail(entity);
        }

        public bool DeleteMovie(long id)
        {
            var item = Context.Movies
                .Include(o => o.Persons)
                .Include(o => o.Names)
                .Include(o => o.Rates)
                .FirstOrDefault(o => o.ID == id);

            if (item == null)
                return false;

            item.Persons.Clear();
            item.Names.Clear();
            item.Rates.Clear();

            Context.Movies.Remove(item);
            Context.SaveChanges();

            return true;
        }

        public Movie UpdateMovie(long id, MovieInput newData)
        {
            var item = Context.Movies
                .Include(o => o.Names)
                .Include(o => o.Persons)
                .FirstOrDefault(o => o.ID == id);

            if (item == null)
                return null;

            if (!string.IsNullOrEmpty(newData.OriginalName))
                item.OriginalName = newData.OriginalName;

            if (newData.Genre > 0)
                item.GenreID = newData.Genre;

            if (newData.Length > 0)
                item.Length = newData.Length;

            if (!string.IsNullOrEmpty(newData.Country))
                item.Country = newData.Country;

            if (!string.IsNullOrEmpty(newData.Description))
                item.Description = newData.Description;

            if (!string.IsNullOrEmpty(newData.TitleImageUrl))
                item.TitleImage = newData.TitleImageUrl;

            if (newData.Names?.Count > 0)
            {
                item.Names.Clear();

                foreach (var name in newData.Names)
                {
                    item.Names.Add(new Domain.Entity.MovieName()
                    {
                        Lang = name.Lang,
                        Name = name.Name
                    });
                }
            }

            if (newData.Actors != null || newData.Directors != null)
            {
                var currentActors = item.Persons.Where(o => o.Type == MoviePersonType.Actor).Select(o => o.PersonID).ToList();
                var currentDirectors = item.Persons.Where(o => o.Type == MoviePersonType.Director).Select(o => o.PersonID).ToList();

                item.Persons.Clear();

                foreach(var actor in newData.Actors ?? currentActors)
                {
                    item.Persons.Add(new MoviePersonEntity() { PersonID = actor, Type = MoviePersonType.Actor });
                }

                foreach(var director in newData.Directors ?? currentDirectors)
                {
                    item.Persons.Add(new MoviePersonEntity() { PersonID = director, Type = MoviePersonType.Director });
                }
            }

            Context.SaveChanges();
            return FindMovieByID(id);
        }
    }
}
