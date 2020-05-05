using AutoMapper;
using Microsoft.Extensions.Options;
using MovieDatabase.API.Models.Auth;
using MovieDatabase.Common.Helpers;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Users;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.API.Services
{
    public class UsersService : IDisposable
    {
        private UsersRepository Repository { get; }
        private IMapper Mapper { get; }
        private AuthSettings AuthSettings { get; }
        private MailService MailService { get; }

        public UsersService(UsersRepository repository, IMapper mapper, IOptions<AuthSettings> options, MailService mailService)
        {
            Repository = repository;
            Mapper = mapper;
            AuthSettings = options.Value;
            MailService = mailService;
        }

        public PaginatedData<SimpleUser> GetUsersList(UserSearchRequest request)
        {
            var result = new PaginatedData<SimpleUser>()
            {
                PageNumber = request.Page
            };
            
            var query = Repository.GetUserList(request.Username);
            result.TotalItemsCount = query.Count();

            var skip = request.Page * request.Limit;
            query = query.Skip(skip).Take(request.Limit);

            result.CanNext = skip + request.Limit < result.TotalItemsCount;
            result.CanPrev = skip != 0;

            result.Data = Mapper.Map<List<SimpleUser>>(query.ToList());
            return result;
        }

        public async Task<SimpleUser> RegisterAsync(RegisterRequest request)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var authCode = StringHelper.CreateRandomString(AuthSettings.AuthKeyLength);

            var user = Repository.CreateRegisteredUser(request.Username, password, request.Email, authCode);

            await MailService.SendRegisterEmailAsync(request.Username, authCode, request.Email);
            return Mapper.Map<SimpleUser>(user);
        }

        public void ConfirmRegister(string code)
        {
            Repository.ConfirmUserRegistration(code);
        }

        public User GetUserDetail(long id)
        {
            var user = Repository.FindUserById(id);
            return user == null ? null : Mapper.Map<User>(user);
        }

        public User UpdateUser(long id, UserEditRequest request)
        {
            var user = Repository.UpdateUser(id, request.Email, request.Username);
            return user == null ? null : Mapper.Map<User>(user);
        }

        public User ChangePassword(long id, PasswordChangeRequest request)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = Repository.ChangePassword(id, password);
            return user == null ? null : Mapper.Map<User>(user);
        }

        public bool DeleteUser(long id)
        {
            if (!Repository.UserIDExists(id))
                return false;

            Repository.DeleteUser(id);
            return true;
        }

        public User ChangeUserRole(long id, RoleChangeRequest request)
        {
            var user = Repository.ChangeUserRole(id, request.Role);
            return user == null ? null : Mapper.Map<User>(user);
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
