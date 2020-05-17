using MovieDatabase.Data.Enums;

namespace MovieDatabase.Data.Models.Search
{
    public class PersonSearchResult : SearchResultBase
    {
        public PersonSearchResult()
        {
            Type = SearchResultType.Person;
        }

        /// <summary>
        /// Unique ID of person.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Name of person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Surname of person.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Profile picture of person.
        /// </summary>
        public string ProfilePictureUrl { get; set; }

        /// <summary>
        /// Age of person.
        /// </summary>
        public int Age { get; set; }
    }
}
