using MovieDatabase.Data.Enums;

namespace MovieDatabase.Data.Models
{
    public abstract class SearchResultBase
    {
        public SearchResultType Type { get; set; }
    }
}
