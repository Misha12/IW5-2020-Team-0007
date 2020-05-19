using MovieDatabase.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDatabase.Data.Entity
{
    [Table("Users")]
    public class User : EntityBase<long>
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(512)]
        public string Password { get; set; }

        [Required]
        public Roles Role { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; }

        [StringLength(255)]
        public string AuthCode { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        public ISet<RefreshToken> RefreshTokens { get; set; }

        public ISet<Rating> Ratings { get; set; }

        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
            Ratings = new HashSet<Rating>();
        }
    }
}
