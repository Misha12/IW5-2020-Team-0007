using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class MovieSearchRequest : PaginatedRequest
    {
        public string Name { get; set; }

        [GenreIDsExists(AllowNulls = true, ErrorMessage = "Neplatný seznam žánrů. Některý žánr neexistuje.")]
        public List<int> GenreIds { get; set; }
        public string Country { get; set; }
        public long? LengthFrom { get; set; }
        public long? LengthTo { get; set; }
    }
}
