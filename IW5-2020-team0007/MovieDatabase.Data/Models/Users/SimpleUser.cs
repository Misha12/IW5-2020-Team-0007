using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MovieDatabase.Data.Models.Users
{
    public class SimpleUser
    {
        public long ID { get; set; }
        public string Username { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Roles Role { get; set; }
    }
}
