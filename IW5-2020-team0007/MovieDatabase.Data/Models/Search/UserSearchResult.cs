using MovieDatabase.Data.Enums;
using System;

namespace MovieDatabase.Data.Models.Search
{
    public class UserSearchResult : SearchResultBase
    {
        public UserSearchResult()
        {
            Type = SearchResultType.User;
        }

        public long ID { get; set; }
        public string Username { get; set; }
        public DateTime MemberFrom { get; set; }
    }
}
