using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Domain.Entity
{
    public class Rate : EntityBase<long>
    {
        public long MovieID { get; set; }
        
        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }
        
        public int Score { get; set; }
        public string Text { get; set; }
    }
}
