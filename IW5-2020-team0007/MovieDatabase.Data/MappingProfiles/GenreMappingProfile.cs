using AutoMapper;
using MovieDatabase.Common.Extensions;

using GenreEntity = MovieDatabase.Data.Entity.Genre;

namespace MovieDatabase.Data.MappingProfiles
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<GenreEntity, Models.Genres.SimpleGenre>();

            CreateMap<GenreEntity, Models.Genres.Genre>()
                .MapMember(dst => dst.MoviesCount, src => src.Movies.Count);
        }
    }
}
