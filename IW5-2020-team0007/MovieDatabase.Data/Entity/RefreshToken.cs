using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("RefreshTokens")]
    public class RefreshToken
    {
        [Key]
        [StringLength(512)]
        public string Token { get; set; }

        public long UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
