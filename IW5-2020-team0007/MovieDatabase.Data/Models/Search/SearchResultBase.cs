using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MovieDatabase.Data.Models.Search
{
    [KnownType(typeof(MovieSearchResult))]
    [KnownType(typeof(PersonSearchResult))]
    [KnownType(typeof(RatingSearchResult))]
    [KnownType(typeof(UserSearchResult))]
    public class SearchResultBase
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SearchResultType Type { get; set; }
    }
}
