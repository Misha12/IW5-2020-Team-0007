using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("MovieNames")]
    public class MovieName : EntityBase<long>
    {
        [StringLength(5)]
        public string Lang { get; set; }

        public long MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
    }
}
