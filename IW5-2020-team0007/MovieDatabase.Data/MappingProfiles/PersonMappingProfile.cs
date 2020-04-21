using AutoMapper;
using MovieDatabase.Common.Extensions;
using MovieDatabase.Data.Enums;
using MovieDatabase.Data.Models.Persons;
using System;
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
                .MapMember(dst => dst.Age, src => ComputeAge(src.Birthdate))
                .MapMember(dst => dst.ActingIn, src => src.Movies.Where(o => o.Type == MoviePersonType.Actor))
                .MapMember(dst => dst.DirectedIn, src => src.Movies.Where(o => o.Type == MoviePersonType.Director));
        }
        
        /// <see cref="https://github.com/Misha12/GrillBot/blob/master/Grillbot/Database/Entity/Birthday.cs"/>
        private int ComputeAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
