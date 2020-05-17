using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Persons
{
    public class CreatePersonRequest
    {
        /// <summary>
        /// Name of person.
        /// </summary>
        [Required(ErrorMessage = "A person's name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Surname of person.
        /// </summary>
        [Required(ErrorMessage = "The person's last name is required.")]
        public string Surname { get; set; }

        /// <summary>
        /// URL to profile picture to person. 
        /// </summary>
        [Url(ErrorMessage = "Invalid person profile picture address.")]
        public string ProfilePictureUrl { get; set; }

        /// <summary>
        /// DateTime of person birth.
        /// </summary>
        [Required(ErrorMessage = "The person's date of birth is required.")]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
    }
}
