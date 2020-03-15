using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Domain.Entity
{
    public class Person : EntityBase<long>
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Surname { get; set; }
        public string ProfilePicture { get; set; }

        public ISet<MoviePerson> InMovies { get; set; }

        public Person()
        {
            InMovies = new HashSet<MoviePerson>();
        }
    }
}
