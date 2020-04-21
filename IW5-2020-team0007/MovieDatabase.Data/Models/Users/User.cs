using MovieDatabase.Data.Models.Ratings;
using System;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Users
{
    public class User : SimpleUser
    {
        public DateTime RegisteredAt { get; set; }
        public string Email { get; set; }

        public List<Rating> Ratings { get; set; }
    }
}
