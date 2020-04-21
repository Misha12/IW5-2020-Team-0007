using MovieDatabase.Data.Models.Common;

namespace MovieDatabase.Data.Models.Users
{
    public class UserSearchRequest : PaginatedRequest
    {
        public string Username { get; set; }
    }
}
