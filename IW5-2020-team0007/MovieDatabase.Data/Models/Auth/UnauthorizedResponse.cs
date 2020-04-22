using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MovieDatabase.Data.Models.Auth
{
    public class UnauthorizedResponse
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public LoginState State { get; set; }
    }
}
