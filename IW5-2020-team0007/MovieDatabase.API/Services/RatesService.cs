using Microsoft.EntityFrameworkCore;
using MovieDatabase.API.Models;
using MovieDatabase.Domain;
using MovieDatabase.Domain.DTO;
using System.Collections.Generic;
using System.Linq;
using DBRate = MovieDatabase.Domain.Entity.Rate;

namespace MovieDatabase.API.Services
{
    public class RatesService
    {
        private MovieDatabaseContext Context { get; }

        public RatesService(MovieDatabaseContext context)
        {
            Context = context;
        }

        public List<Rate> GetRateList(long? movieID)
        {
            List<DBRate> rates = null;

            if (movieID == null)
            {
                rates = Context.Rates.ToList();
            }
            else
            {
                var movie = Context.Movies.Include(o => o.Rates).FirstOrDefault(o => o.ID == movieID.Value);

                if (movie == null)
                    return null;

                rates = movie.Rates.ToList();
            }

            return rates.Select(o => new Rate(o)).ToList();
        }

        public RateDetail FindRateByID(long movieID, long rateID)
        {
            var item = Context.Rates
                .Include(o => o.Movie)
                .ThenInclude(o => o.Genre)
                .FirstOrDefault(o => o.MovieID == movieID && o.ID == rateID);

            return item == null ? null : new RateDetail(item);
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

            return new RateDetail(entity);
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
            return new RateDetail(rate);
        }
    }
}
;