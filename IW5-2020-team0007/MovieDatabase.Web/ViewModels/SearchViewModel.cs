using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Web.ViewModels
{
    public class SearchViewModel
    {
        public String keyword { get; set; }
        public PaginatedDataOfSearchResult SearchResultBase { get; set; }
        public bool? LoginSuccess { get; set; }
    }
}
