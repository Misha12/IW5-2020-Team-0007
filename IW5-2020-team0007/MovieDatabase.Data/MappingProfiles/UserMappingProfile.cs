using AutoMapper;
using MovieDatabase.Common.Extensions;
using MovieDatabase.Data.Models.Users;
using UserEntity = MovieDatabase.Data.Entity.User;

namespace MovieDatabase.Data.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, User>()
                .MapMember(dst => dst.Role, src => src.Role.ToString());

            CreateMap<UserEntity, SimpleUser>()
                .MapMember(dst => dst.Role, src => src.Role.ToString());
        }
    }
}
