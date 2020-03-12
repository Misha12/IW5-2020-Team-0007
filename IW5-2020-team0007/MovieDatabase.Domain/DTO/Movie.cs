namespace MovieDatabase.Domain.DTO
{
    public class Movie : ItemBase<long>
    {
        public string OriginalName { get; set; }
        public string TitleImage { get; set; }
        public Genre Genre { get; set; }
        public long Length { get; set; }
        public string Country { get; set; }

        public static Movie FromEntity(Entity.Movie movie)
        {
            if (movie == null)
                return null;

            return new Movie()
            {
                Country = movie.Country,
                Genre = Genre.FromEntity(movie.Genre),
                ID = movie.ID,
                Length = movie.Length,
                OriginalName = movie.OriginalName,
                TitleImage = movie.TitleImage
            };
        }
    }
}
