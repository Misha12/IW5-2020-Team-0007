using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MovieDatabase.Data.Models.Auth
{
    /// <summary>
    /// Unauthorized data response.
    /// </summary>
    public class UnauthorizedResponse
    {
        /// <summary>
        /// Error description.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LoginState State { get; set; }
    }
}
