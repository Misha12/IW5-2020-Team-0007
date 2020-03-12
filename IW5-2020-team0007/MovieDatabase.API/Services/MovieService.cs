using Microsoft.EntityFrameworkCore;
using MovieDatabase.API.Models;
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

        private IQueryable<MovieEntity> GetBaseQuery(bool includeGenre = false, bool includeNames = false)
        {
            var query = Context.Movies.AsQueryable();

            if (includeGenre)
                query = query.Include(o => o.Genre);

            if (includeNames)
                query = query.Include(o => o.Names);

            return query;
        }

        public List<Movie> GetMovieList(string searchName, int[] genres, long? lengthFrom, long? lengthTo, string[] countries)
        {
            var query = GetBaseQuery(true);

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

        public Movie FindMovieById(long id)
        {
            var item = GetBaseQuery(true, true).FirstOrDefault(o => o.ID == id);
            return item == null ? null : new MovieWithNames(item);
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

            if(data.Names?.Count > 0)
            {
                foreach(var name in data.Names)
                {
                    entity.Names.Add(new Domain.Entity.MovieName() { Lang = name.Lang, Name = name.Name });
                }
            }

            Context.Movies.Add(entity);
            Context.SaveChanges();

            return new MovieWithNames(entity);
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

        public Movie UpdateMovie(long id, MovieInput newData)
        {
            var item = GetBaseQuery(false, true).FirstOrDefault(o => o.ID == id);

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

            if(newData.Names?.Count > 0)
            {
                item.Names.Clear();

                foreach(var name in newData.Names)
                {
                    item.Names.Add(new Domain.Entity.MovieName()
                    {
                        Lang = name.Lang,
                        Name = name.Name
                    });
                }
            }

            Context.SaveChanges();
            return FindMovieById(id);
        }
    }
}
