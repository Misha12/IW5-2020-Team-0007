namespace MovieDatabase.Domain.DTO
{
    public class MovieName
    {
        /// <summary>
        /// Jazyk, ve kterém je přeložen název.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Překlad názvu filmu ve specifikovaném jazyce.
        /// </summary>
        public string Name { get; set; }
    }
}
