using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MovieDatabase.Data.Models.Search
{
    /// <summary>
    /// Base data of search.
    /// </summary>
    [KnownType(typeof(MovieSearchResult))]
    [KnownType(typeof(PersonSearchResult))]
    [KnownType(typeof(RatingSearchResult))]
    [KnownType(typeof(UserSearchResult))]
    public class SearchResultBase
    {
        /// <summary>
        /// Type of entity.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SearchResultType Type { get; set; }
    }
}
