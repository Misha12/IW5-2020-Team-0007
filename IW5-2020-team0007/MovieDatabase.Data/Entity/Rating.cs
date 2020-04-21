using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("Ratings")]
    public class Rating : EntityBase<long>
    {
        public long MovieID { get; set; }

        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        public long UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public int Score { get; set; }

        public string Text { get; set; }
    }
}
