using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class ClientFacade
    {
        private readonly HttpClient httpClient;
        public ClientFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<AuthToken> LoginAsync(LoginRequest user)
        {

            AuthControllerClient _client = new AuthControllerClient(httpClient);
            return await _client.LoginAsync(user);
        }
        public async Task<AuthToken> RefreshAsync(String token, String refresh)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            AuthControllerClient _client = new AuthControllerClient(httpClient);
            return await _client.RefreshTokenAsync(refresh);
        }
    }
}
