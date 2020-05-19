using AutoMapper;
using MovieDatabase.Common.Extensions;
using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Persons;
using System.Linq;
using PersonEntity = MovieDatabase.Data.Entity.Person;

namespace MovieDatabase.Data.MappingProfiles
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<PersonEntity, SimplePerson>();

            CreateMap<PersonEntity, Person>()
                .MapMember(dst => dst.Age, src => src.Birthdate.ComputeAge())
                .MapMember(dst => dst.ActingIn, src => src.Movies.Where(o => o.Type == MoviePersonType.Actor).Select(o => o.Movie))
                .MapMember(dst => dst.DirectedIn, src => src.Movies.Where(o => o.Type == MoviePersonType.Director).Select(o => o.Movie));

            CreateMap<PersonEntity, PersonFilterItem>()
                .MapMember(dst => dst.NameSurname, src => $"{src.Name} {src.Surname}");
        }
    }
}
