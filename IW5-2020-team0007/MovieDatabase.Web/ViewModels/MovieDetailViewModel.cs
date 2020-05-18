using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class MovieDetailViewModel
    {
        public Movie DetailMovieModel { get; set; }
        public PaginatedDataOfRating ListRatingModel { get; set; }
        public List<Genre> Genres { get; set; }
        public List<PersonFilterItem> Persons { get; set; }
        public bool? SaveSuccess { get; set; }

        public int AvgRatingScore => Convert.ToInt32(ListRatingModel.Data.Average(o => o.Score));
    }
}
