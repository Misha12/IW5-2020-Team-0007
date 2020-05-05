using MovieDatabase.Data.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Users
{
    public class RoleChangeRequest
    {
        [Required(ErrorMessage = "Výběr role je povinný.")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Roles Role { get; set; }
    }
}
