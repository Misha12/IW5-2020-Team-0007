using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class SearchFacade
    {
        private readonly HttpClient httpClient;
        public SearchFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PaginatedDataOfSearchResult> SearchAsync(String keyword,int? limit, int? page)
        {
            SearchControllerClient searchControllerClient = new SearchControllerClient(httpClient);
            return await searchControllerClient.SearchAsync(keyword, limit, page);
        }
    }
}
