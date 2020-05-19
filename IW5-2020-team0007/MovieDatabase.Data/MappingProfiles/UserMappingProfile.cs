using AutoMapper;
using MovieDatabase.Data.Models.Users;
using UserEntity = MovieDatabase.Data.Entity.User;

namespace MovieDatabase.Data.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, SimpleUser>();
            CreateMap<UserEntity, User>();
        }
    }
}
