using System.Collections.Generic;

namespace MovieDatabase.Data.Models.Common
{
    public class PaginatedData<TModel>
    {
        public List<TModel> Data { get; set; }
        
        public int PageNumber { get; set; }
        public long TotalItemsCount { get; set; }
        
        public bool CanNext { get; set; }
        public bool CanPrev { get; set; }
    }
}
