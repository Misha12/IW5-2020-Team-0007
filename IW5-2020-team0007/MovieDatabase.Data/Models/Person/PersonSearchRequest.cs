using MovieDatabase.Data.Models.Common;

namespace MovieDatabase.Data.Models.Person
{
    public class PersonSearchRequest : PaginatedRequest
    {
        public string NameSurname { get; set; }
    }
}
