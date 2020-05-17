using MovieDatabase.Data.Models.Ratings;
using System;
using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Users
{
    /// <summary>
    /// Detailed information about registered user.
    /// </summary>
    public class User : SimpleUser
    {
        /// <summary>
        /// Datetime of registered user.
        /// </summary>
        public DateTime RegisteredAt { get; set; }

        /// <summary>
        /// Email of user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// List of user ratings.
        /// </summary>
        public List<Rating> Ratings { get; set; }
    }
}
