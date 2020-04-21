using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Person
{
    public class EditPersonRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ProfilePictureUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
    }
}
