using System;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Persons
{
    public class EditPersonRequest
    {
        /// <summary>
        /// New name of person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// New surname of person.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// URL to profile picture to person. 
        /// </summary>
        [Url(ErrorMessage = "Invalid profile image address format.")]
        public string ProfilePictureUrl { get; set; }

        /// <summary>
        /// DateTime of person birth.
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? Birthday { get; set; }
    }
}
