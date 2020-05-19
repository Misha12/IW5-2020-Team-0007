using AutoMapper;
using RatingEntity = MovieDatabase.Data.Entity.Rating;
using MovieDatabase.Data.Models.Ratings;
using MovieDatabase.Common.Extensions;

namespace MovieDatabase.Data.MappingProfiles
{
    public class RatingMappingProfile : Profile
    {
        public RatingMappingProfile()
        {
            CreateMap<RatingEntity, Rating>()
                .MapMember(dst => dst.Author, src => src.Anonymous ? null : src.User);
        }
    }
}
