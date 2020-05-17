using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class RoleChangeRequest
    {
        /// <summary>
        /// New role of user.
        /// </summary>
        [Required(ErrorMessage = "Role selection is required.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Roles Role { get; set; }
    }
}
