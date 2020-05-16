using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class UserFacade
    {
        private readonly IUsersControllerClient _userClient;
        private readonly IClient _client;
        public UserFacade(IUsersControllerClient usersControllerClient, IClient client)
        {
            _userClient = usersControllerClient;
            _client = client;
        }
        public async Task<AuthToken> LoginAsync(LoginRequest user)
        {
            return await _client.LoginAsync(user);
        }
        public async Task<SimpleUser> InsertAsync(RegisterRequest newUser)
        {
            return await _userClient.RegisterAsync(newUser);
        }
        public async Task DeleteUserAsync(long ID)
        {
            await _userClient.DeleteUserAsync(ID);
        }
    }
}
