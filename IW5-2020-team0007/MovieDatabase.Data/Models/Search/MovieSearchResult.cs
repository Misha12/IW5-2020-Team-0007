using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Movies;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Search
{
    public class MovieSearchResult : SearchResultBase
    {
        public MovieSearchResult()
        {
            Type = SearchResultType.Movie;
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public List<MovieName> Names { get; set; }
        public string Country { get; set; }
        public long Length { get; set; }
    }
}
