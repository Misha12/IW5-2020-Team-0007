using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.API.Models.Auth
{
    public class AuthSettings
    {
        public int ExpirationDays { get; set; }
        public string Secret { get; set; }
    }
}
