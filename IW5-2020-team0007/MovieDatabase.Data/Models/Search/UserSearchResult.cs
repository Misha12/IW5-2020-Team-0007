using MovieDatabase.Data.Enums;
using System;

namespace MovieDatabase.Data.Models.Search
{
    public class UserSearchResult
    {
        /// <summary>
        /// Unique ID of user.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Username of user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// DateTime of user registration.
        /// </summary>
        public DateTime MemberFrom { get; set; }
    }
}
