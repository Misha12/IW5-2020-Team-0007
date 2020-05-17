using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class RateFacade
    {
        private readonly HttpClient httpClient;
        public RateFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Rating> CreateRateAsync(String token, CreateRateRequest request)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            RatesControllerClient _client = new RatesControllerClient(httpClient);
            return await _client.CreateRateAsync(request);
        }
        public async Task<Rating> UpdateRateAsync(String token, long ID, EditRatingRequest request)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            RatesControllerClient _client = new RatesControllerClient(httpClient);
            return await _client.UpdateRateAsync(ID, request);
        }

        public async Task DeleteRateAsync(String token, long ID)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            RatesControllerClient _client = new RatesControllerClient(httpClient);
            await _client.DeleteRateAsync(ID);
        }
        public async Task<PaginatedDataOfRating> GetRatingsListAsync(String token, IEnumerable<long> ID, String text, int? scoreFrom, int? scoreTo, int? limit, int? page)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            RatesControllerClient _client = new RatesControllerClient(httpClient);
            return await _client.GetRatingsListAsync(ID, text, scoreFrom, scoreTo, limit, page);
        }
    }
}
