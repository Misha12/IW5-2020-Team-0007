using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Movies
{
    public class MovieNameRequest
    {
        [Required(ErrorMessage = "Jazyk překladu názvu je požadován.")]
        public string Lang { get; set; }

        [Required(ErrorMessage = "Přeložený název filmu je požadován.")]
        public string Name { get; set; }
    }
}
