using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Common
{
    /// <summary>
    /// Request data for pagination.
    /// </summary>
    public class PaginatedRequest
    {
        /// <summary>
        /// Maximal count of records at one page.
        /// </summary>
        [Range(0, 200, ErrorMessage = "The limit is out of range.")]
        public int Limit { get; set; } = 50;
        
        /// <summary>
        /// Requested page number.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Page number is out of range.")]
        public int Page { get; set; }
    }
}
