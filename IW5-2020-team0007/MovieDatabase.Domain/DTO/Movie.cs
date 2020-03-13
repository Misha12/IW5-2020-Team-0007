namespace MovieDatabase.Domain.DTO
{
    public class Movie : ItemBase<long>
    {
        public string OriginalName { get; set; }
        public string TitleImageUrl { get; set; }
        public Genre Genre { get; set; }
        public long Length { get; set; }
        public string Country { get; set; }

        public Movie(Entity.Movie movie) : base(movie.ID)
        {
            Country = movie.Country;
            Genre = new Genre(movie.Genre);
            Length = movie.Length;
            OriginalName = movie.OriginalName;
            TitleImageUrl = movie.TitleImage;
        }
    }
}
