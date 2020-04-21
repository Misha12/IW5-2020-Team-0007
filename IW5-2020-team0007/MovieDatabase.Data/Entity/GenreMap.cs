using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("GenreMap")]
    public class GenreMap
    {
        public long MovieID { get; set; }
        public int GenreID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }
    }
}
