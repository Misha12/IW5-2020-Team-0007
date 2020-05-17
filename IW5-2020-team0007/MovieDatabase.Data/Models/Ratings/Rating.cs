using MovieDatabase.Data.Models.Movies;
using MovieDatabase.Data.Models.Users;

namespace MovieDatabase.Data.Models.Ratings
{
    public class Rating
    {
        public long ID { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }

        public SimpleMovie Movie { get; set; }
        public SimpleUser Author { get; set; }
    }
}
