using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Domain.Entity
{
    public class MovieName : EntityBase<string>
    {
        public long MovieID { get; set; }
        
        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }
        
        [StringLength(255)]
        public string Name { get; set; }
    }
}
