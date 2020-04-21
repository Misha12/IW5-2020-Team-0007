using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Persons
{
    public class EditPersonRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        [Url(ErrorMessage = "Neplatný formát adresy profilového obrázku.")]
        public string ProfilePictureUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
    }
}
