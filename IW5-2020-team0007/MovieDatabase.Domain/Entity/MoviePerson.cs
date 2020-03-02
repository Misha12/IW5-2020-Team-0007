using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Domain.Entity
{
    public class MoviePerson
    {
        public long MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        public long PersonID { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }
    }
}
