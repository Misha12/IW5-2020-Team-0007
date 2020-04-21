using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Movies;
using MovieDatabase.Data.Models.Users;

namespace MovieDatabase.Data.Models.Search
{
    public class RatingSearchResult : SearchResultBase
    {
        public RatingSearchResult()
        {
            Type = SearchResultType.Rating;
        }

        public string ShortText { get; set; }
        public int Score { get; set; }
        public SimpleMovie Movie { get; set; }
        public SimpleUser User { get; set; }
    }
}
