using MovieDatabase.Data.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Search
{
    public class SearchRequest : PaginatedRequest
    {
        /// <summary>
        /// Requested keyword, that must contains string in search.
        /// </summary>
        [Required(ErrorMessage = "Entering search input is required.")]
        public string Keyword { get; set; }
    }
}
