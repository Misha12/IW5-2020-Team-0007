using MovieDatabase.Data.Enums;

namespace MovieDatabase.Data.Models.Search
{
    public class PersonSearchResult : SearchResultBase
    {
        public PersonSearchResult()
        {
            Type = SearchResultType.Person;
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int Age { get; set; }
    }
}
