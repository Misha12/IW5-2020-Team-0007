using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class UserFacade
    {
        private readonly HttpClient httpClient;
        public UserFacade(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<SimpleUser> InsertAsync(RegisterRequest newUser)
        {
            UsersControllerClient _userClient = new UsersControllerClient(httpClient);
            return await _userClient.RegisterAsync(newUser);
        }
        public async Task<User> CurrentUserAsync(String token)
        {
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            UsersControllerClient _client = new UsersControllerClient(httpClient);
            return await _client.GetCurrentUserDetailAsync();
        }
        public async Task<PaginatedDataOfSimpleUser> GetUsersListAsync(String token, String userName, int? limit, int? page)
        {
            httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer", token);
            UsersControllerClient _client = new UsersControllerClient(httpClient);
            return await _client.GetUsersListAsync(userName,limit,page);
        }
        public async Task<ValidationProblemDetails> CurrentUserUpdate(String token, UserEditRequest userEditRequest)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            UsersControllerClient _client = new UsersControllerClient(httpClient);
            return await _client.UpdateCurrentUserAsync(userEditRequest);
        }
        //todo logout
        public async Task DeleteUserAsync(String token, long ID)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            UsersControllerClient _userClient = new UsersControllerClient(httpClient);
            await _userClient.DeleteUserAsync(ID);
        }
    }
}
