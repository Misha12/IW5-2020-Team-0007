using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MovieDatabase.Data.Models.Users
{
    /// <summary>
    /// Simple data of registered user.
    /// </summary>
    public class SimpleUser
    {
        /// <summary>
        /// Unique identifier of registered user.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Username of registered user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Current user role.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Roles Role { get; set; }
    }
}
