using AutoMapper;
using MovieDatabase.Common.Extensions;
using MovieDatabase.Data.Enums;
using System.Linq;
using MovieEntity = MovieDatabase.Data.Entity.Movie;
using MovieNameEntity = MovieDatabase.Data.Entity.MovieName;

namespace MovieDatabase.Data.MappingProfiles
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<MovieNameEntity, Models.Movies.MovieName>();
            CreateMap<MovieEntity, Models.Movies.SimpleMovie>();

            CreateMap<MovieEntity, Models.Movies.Movie>()
                .MapMember(dst => dst.TranslatedNames, src => src.Names)
                .MapMember(dst => dst.Actors, src => src.Persons.Where(o => o.Type == MoviePersonType.Actor).Select(o => o.Person))
                .MapMember(dst => dst.Directors, src => src.Persons.Where(o => o.Type == MoviePersonType.Director).Select(o => o.Person));
        }
    }
}
