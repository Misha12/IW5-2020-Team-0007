using MovieDatabase.Data.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Search
{
    public class SearchRequest : PaginatedRequest
    {
        [Required(ErrorMessage = "Zadání vstupu pro vyhledávání je povinné.")]
        public string Keyword { get; set; }
    }
}
