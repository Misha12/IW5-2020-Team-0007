using MovieDatabase.Data.Models.Movies;
using System;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Person
{
    public class Person : SimplePerson
    {
        public DateTime Birthdate { get; set; }
        public int Age { get; set; }

        public List<SimpleMovie> ActingIn { get; set; }
        public List<SimpleMovie> DirectedIn { get; set; }
    }
}
