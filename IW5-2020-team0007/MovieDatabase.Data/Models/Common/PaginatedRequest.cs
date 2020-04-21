using System.ComponentModel.DataAnnotations;

namespace MovieDatabase.Data.Models.Common
{
    public class PaginatedRequest
    {
        [Range(0, 200, ErrorMessage = "Limit je mimo povolený rozsah")]
        public int Limit { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Číslo stránky je mimo povolený rozsah.")]
        public int Page { get; set; }
    }
}
