using MovieDatabase.Data.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Ratings
{
    public class RatingSearchRequest : PaginatedRequest
    {
        public List<long> MovieIDs { get; set; }
        public string TextContains { get; set; }

        [Range(0, 100, ErrorMessage = "Povolený rozsah číselného hodnocení je mezi 0 až 100 body.")]
        public int? ScoreFrom { get; set; }

        [Range(0, 100, ErrorMessage = "Povolený rozsah číselného hodnocení je mezi 0 až 100 body.")]
        public int? ScoreTo { get; set; }
    }
}
