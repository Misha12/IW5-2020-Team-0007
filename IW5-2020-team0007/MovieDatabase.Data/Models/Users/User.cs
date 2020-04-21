using System;

namespace MovieDatabase.Data.Models.Users
{
    public class User : SimpleUser
    {
        public DateTime RegisteredAt { get; set; }
        public string Email { get; set; }
        
        // TODO SimpleRating
    }
}
