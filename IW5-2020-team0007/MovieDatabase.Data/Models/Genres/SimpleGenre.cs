namespace MovieDatabase.Data.Models.Genres
{
    /// <summary>
    /// Basic information about genre.
    /// </summary>
    public class SimpleGenre
    {
        /// <summary>
        /// Unique identificator of genre.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of genre.
        /// </summary>
        public string Name { get; set; }
    }
}
