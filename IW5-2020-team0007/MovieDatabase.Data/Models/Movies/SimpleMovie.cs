using MovieDatabase.Data.Models.Genres;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Movies
{
    public class SimpleMovie
    {
        public long ID { get; set; }
        public string OriginalName { get; set; }
        public string TitleImageUrl { get; set; }
        public string Country { get; set; }
        public long Length { get; set; }
        public List<SimpleGenre> Genres { get; set; }
        public int CreatedYear { get; set; }
    }
}
