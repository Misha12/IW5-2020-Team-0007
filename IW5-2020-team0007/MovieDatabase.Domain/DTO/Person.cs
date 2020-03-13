using System;

namespace MovieDatabase.Domain.DTO
{
    public class Person
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
        public string ProfilePicture { get; set; }
    }
}
