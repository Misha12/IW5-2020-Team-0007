namespace MovieDatabase.Data.Models.Genres
{
    /// <summary>
    /// Detail information of genre.
    /// </summary>
    public class Genre : SimpleGenre
    {
        /// <summary>
        /// Count of movies associated with genres.
        /// </summary>
        public int MoviesCount { get; set; }
    }
}
