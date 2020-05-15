using MovieDatabase.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.BL.Web.Facades
{
    public class PersonFacade
    {
        private readonly UsersControllerClient _userClient;
        
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
