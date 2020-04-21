using MovieDatabase.Data.Models.Common;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Ratings
{
    public class RatingSearchRequest : PaginatedRequest
    {
        public List<long> MovieIDs { get; set; }
        public string TextContains { get; set; }
        public int ScoreFrom { get; set; }
        public int ScoreTo { get; set; }
    }
}
