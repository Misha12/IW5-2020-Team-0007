using AutoMapper;
using RatingEntity = MovieDatabase.Data.Entity.Rating;
using MovieDatabase.Data.Models.Ratings;

namespace MovieDatabase.Data.MappingProfiles
{
    public class RatingMappingProfile : Profile
    {
        public RatingMappingProfile()
        {
            CreateMap<RatingEntity, Rating>();
        }
    }
}
