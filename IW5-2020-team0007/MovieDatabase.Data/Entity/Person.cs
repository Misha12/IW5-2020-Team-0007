using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("Persons")]
    public class Person : EntityBase<long>
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Surname { get; set; }

        public DateTime Birthdate { get; set; }

        public string ProfilePictureUrl { get; set; }

        public ISet<MoviePerson> Movies { get; set; }

        public Person()
        {
            Movies = new HashSet<MoviePerson>();
        }
    }
}
