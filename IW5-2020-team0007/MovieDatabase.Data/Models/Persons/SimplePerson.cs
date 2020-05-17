namespace MovieDatabase.Data.Models.Persons
{
    /// <summary>
    /// Simple information about person.
    /// </summary>
    public class SimplePerson
    {
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
        /// URL to profile picture to person. 
        /// </summary>
        public string ProfilePictureUrl { get; set; }
    }
}
