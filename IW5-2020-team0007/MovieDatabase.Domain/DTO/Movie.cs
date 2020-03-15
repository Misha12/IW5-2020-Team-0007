namespace MovieDatabase.Domain.DTO
{
    public class Movie : ItemBase<long>
    {
        public string OriginalName { get; set; }
        public string TitleImageUrl { get; set; }
        public Genre Genre { get; set; }
        public long Length { get; set; }
        public string Country { get; set; }
    }
}
