using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MovieDatabase.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserID(this ClaimsPrincipal user)
        {
            return Convert.ToInt64(user.FindFirst(ClaimTypes.Name).Value);
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role).Value;
        }
    }
}
