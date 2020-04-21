using MovieDatabase.Data.Models.Common;

namespace MovieDatabase.Data.Models.Persons
{
    public class PersonSearchRequest : PaginatedRequest
    {
        public string NameSurname { get; set; }
    }
}
