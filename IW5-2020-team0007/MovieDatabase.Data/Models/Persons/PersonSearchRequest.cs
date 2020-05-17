using MovieDatabase.Data.Models.Common;

namespace MovieDatabase.Data.Models.Persons
{
    public class PersonSearchRequest : PaginatedRequest
    {
        /// <summary>
        /// Query request of person.
        /// </summary>
        public string NameSurname { get; set; }
    }
}
