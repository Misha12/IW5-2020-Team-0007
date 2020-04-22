using AutoMapper;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Users;
using MovieDatabase.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieDatabase.API.Services
{
    public class UsersService : IDisposable
    {
        private UsersRepository Repository { get; }
        private IMapper Mapper { get; }

        public UsersService(UsersRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
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

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}
