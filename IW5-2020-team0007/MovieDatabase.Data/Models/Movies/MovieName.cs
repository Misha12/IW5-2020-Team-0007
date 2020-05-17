namespace MovieDatabase.Data.Models.Movies
{
    public class MovieName
    {
        /// <summary>
        /// Language of movie name translation.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Translated movie name.
        /// </summary>
        public string Name { get; set; }
    }
}
