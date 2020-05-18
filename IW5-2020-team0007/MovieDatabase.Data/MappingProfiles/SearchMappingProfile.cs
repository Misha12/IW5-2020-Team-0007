using AutoMapper;
using MovieDatabase.Common.Extensions;
using MovieDatabase.Data.Entity;
using MovieDatabase.Data.Models.Search;

namespace MovieDatabase.Data.MappingProfiles
{
    public class SearchMappingProfile : Profile
    {
        public SearchMappingProfile()
        {
            CreateMap<Movie, MovieSearchResult>();

            CreateMap<Person, PersonSearchResult>()
                .MapMember(dst => dst.Age, src => src.Birthdate.ComputeAge());

            CreateMap<User, UserSearchResult>()
                .MapMember(dst => dst.MemberFrom, src => src.RegisteredAt);

            CreateMap<Rating, RatingSearchResult>()
                .MapMember(dst => dst.User, src => src.Anonymous ? null : src.User)
                .MapMember(dst => dst.ShortText, src => string.IsNullOrEmpty(src.Text) || src.Text.Length < 150 ? src.Text : src.Text.Substring(0, 150));
        }
    }
}
