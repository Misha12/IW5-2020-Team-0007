using System;

namespace MovieDatabase.Domain.DTO
{
    public class Person : ItemBase<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePictureUrl { get; set; }

        public Person(Entity.Person person) : base(person.ID)
        {
            Name = person.Name;
            Surname = person.Surname;
            ProfilePictureUrl = person.ProfilePicture;
        }

        public Person() { }
    }
}
