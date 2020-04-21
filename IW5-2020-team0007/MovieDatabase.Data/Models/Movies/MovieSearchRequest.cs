using MovieDatabase.Data.Models.Common;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    public class MovieSearchRequest : PaginatedRequest
    {
        public string Name { get; set; }
        public List<int> GenreIds { get; set; }
        public string Country { get; set; }

        public long LengthFrom { get; set; }
        public long LengthTo { get; set; }
    }
}
