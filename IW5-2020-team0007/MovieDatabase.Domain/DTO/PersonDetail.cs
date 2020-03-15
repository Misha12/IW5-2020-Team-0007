using System.Collections.Generic;

namespace MovieDatabase.Domain.DTO
{
    public class PersonDetail : Person
    {
        public List<Movie> ActingIn { get; set; }
        public List<Movie> DirectedIn { get; set; }
    }
}
