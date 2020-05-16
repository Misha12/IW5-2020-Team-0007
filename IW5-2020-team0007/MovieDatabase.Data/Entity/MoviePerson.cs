using MovieDatabase.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("MoviePersons")]
    public class MoviePerson : EntityBase<long>
    {
        public long PersonID { get; set; }
        public long MovieID { get; set; }

        [ForeignKey("PersonID")]
        public Person Person { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        public MoviePersonType Type { get; set; }
    }
}
