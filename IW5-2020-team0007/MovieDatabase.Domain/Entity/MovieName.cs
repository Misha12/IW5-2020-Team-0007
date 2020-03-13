using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Domain.Entity
{
    [Table("MovieNames")]
    public class MovieName
    {
        [Key]
        public string Lang { get; set; }

        public long MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        [StringLength(255)]
        public string Name { get; set; }
    }
}
