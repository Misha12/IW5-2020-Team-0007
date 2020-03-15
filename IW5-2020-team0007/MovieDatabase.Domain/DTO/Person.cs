
namespace MovieDatabase.Domain.DTO
{
    public class Person : ItemBase<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
