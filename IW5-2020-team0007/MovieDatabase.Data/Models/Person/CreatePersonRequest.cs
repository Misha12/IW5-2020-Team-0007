using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Person
{
    public class CreatePersonRequest
    {
        [Required(ErrorMessage = "Jméno osoby je vyžadováno.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Příjmené osoby je vyžadováno.")]
        public string Surname { get; set; }

        [Url(ErrorMessage = "Neplatná adresa profilového obrázku osoby.")]
        public string ProfilePictureUrl { get; set; }

        [Required(ErrorMessage = "Datum narození osoby je požadováno.")]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
    }
}
