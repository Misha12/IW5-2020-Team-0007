using System;

namespace MovieDatabase.API.Models
{
    public class PersonInput
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePictureUrl { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Surname);
        }
    }
}
