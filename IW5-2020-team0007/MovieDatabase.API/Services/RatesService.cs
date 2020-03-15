﻿using Microsoft.EntityFrameworkCore;
using MovieDatabase.API.Models;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DBRate = MovieDatabase.Domain.Entity.Rate;

namespace MovieDatabase.API.Services
{
    public class RatesService
    {
        private MovieDatabaseContext Context { get; }
        private IMapper Mapper { get; }

        public RatesService(MovieDatabaseContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public List<Rate> GetRateList(long? movieID, int? scoreFrom, int? scoreTo)
        {
            List<DBRate> rates = null;

            if (movieID == null)
            {
                var query = Context.Rates.AsQueryable();

                if (scoreFrom != null)
                    query = query.Where(o => o.Score >= scoreFrom.Value);

                if (scoreTo != null)
                    query = query.Where(o => o.Score < scoreTo.Value);

                rates = query.ToList();
            }
            else
            {
                var movie = Context.Movies
                    .Include(o => o.Rates)
                    .FirstOrDefault(o => o.ID == movieID.Value);

                if (movie == null)
                    return null;

                rates = movie.Rates.ToList();

                if (scoreFrom != null)
                    rates = rates.Where(o => o.Score >= scoreFrom.Value).ToList();

                if (scoreTo != null)
                    rates = rates.Where(o => o.Score < scoreTo.Value).ToList();
            }

            return Mapper.Map<List<Rate>>(rates);
        }

        public RateDetail FindRateByID(long movieID, long rateID)
        {
            var item = Context.Rates
                .Include(o => o.Movie)
                .ThenInclude(o => o.Genre)
                .FirstOrDefault(o => o.MovieID == movieID && o.ID == rateID);

            return Mapper.Map<RateDetail>(item);
        }

        public RateDetail CreateRate(long movieID, RateInput data)
        {
            var movie = Context.Movies
                .Include(o => o.Genre)
                .Include(o => o.Rates)
                .FirstOrDefault(o => o.ID == movieID);

            if (movie == null)
                return null;

            var entity = new DBRate()
            {
                MovieID = movieID,
                Score = data.GetScore(),
                Text = data.Text
            };

            movie.Rates.Add(entity);
            Context.SaveChanges();

            return FindRateByID(movieID, entity.ID);
        }

        public bool DeleteRate(long movieID, long rateID)
        {
            var rate = Context.Rates
                .FirstOrDefault(o => o.MovieID == movieID && o.ID == rateID);

            if (rate == null)
                return false;

            Context.Rates.Remove(rate);
            Context.SaveChanges();

            return true;
        }

        public RateDetail UpdateRate(long movieID, long rateID, RateInput input)
        {
            var rate = Context.Rates
                .Include(o => o.Movie)
                .ThenInclude(o => o.Genre)
                .FirstOrDefault(o => o.MovieID == movieID && o.ID == rateID);

            if (rate == null)
                return null;

            if (!string.IsNullOrEmpty(input.Text))
                rate.Text = input.Text;

            if (rate.Score != input.GetScore())
                rate.Score = input.GetScore();

            Context.SaveChanges();
            return Mapper.Map<RateDetail>(rate);
        }
    }
}
;