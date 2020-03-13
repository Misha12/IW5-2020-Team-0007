namespace MovieDatabase.Domain.DTO
{
    public class Genre : ItemBase<int>
    {
        public string Name { get; set; }

        public Genre(Entity.Genre genre) : base(genre.ID)
        {
            Name = genre.Name;
        }

        public Genre() { }
    }
}
