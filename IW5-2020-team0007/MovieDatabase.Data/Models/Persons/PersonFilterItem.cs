namespace MovieDatabase.Data.Models.Persons
{
    public class PersonFilterItem
    {
        /// <summary>
        /// Unique ID of person.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Concatenated name and surname of person.
        /// </summary>
        public string NameSurname { get; set; }
    }
}
