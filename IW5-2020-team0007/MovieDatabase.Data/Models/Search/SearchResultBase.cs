using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MovieDatabase.Data.Models.Search
{
    /// <summary>
    /// Base data of search.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Type of entity.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SearchResultType Type { get; set; }

        public MovieSearchResult MovieResult { get; set; }
        public PersonSearchResult PersonResult { get; set; }
        public UserSearchResult UserResult { get; set; }
        public RatingSearchResult RatingResult { get; set; }
    }
}
