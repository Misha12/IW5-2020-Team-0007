namespace MovieDatabase.Domain.DTO
{
    public class Genre : ItemBase<int>
    {
        public string Name { get; set; }

        public static Genre FromEntity(Entity.Genre genre)
        {
            if (genre == null)
                return null;

            return new Genre()
            {
                ID = genre.ID,
                Name = genre.Name
            };
        }
    }
}
