using MovieDatabase.Data.Models.Common;

namespace MovieDatabase.Data.Models.Users
{
    /// <summary>
    /// Filtration parameter of request for user list.
    /// </summary>
    public class UserSearchRequest : PaginatedRequest
    {
        /// <summary>
        /// Request query for username. Value must contains inserted string. Null value skips filtration.
        /// </summary>
        public string Username { get; set; }
    }
}
