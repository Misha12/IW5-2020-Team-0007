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
        public DateTime Birthdate { get; set; }
        public string ProfilePicture { get; set; }

        public ISet<MoviePerson> ActorInMovies { get; set; }
        public ISet<MoviePerson> DirectorOfMovies { get; set; }

        public Person()
        {
            ActorInMovies = new HashSet<MoviePerson>();
            DirectorOfMovies = new HashSet<MoviePerson>();
        }
    }
}
