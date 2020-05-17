﻿using AutoMapper;
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
                .MapMember(dst => dst.ActingIn, src => src.Movies.Where(o => o.Type == MoviePersonType.Actor))
                .MapMember(dst => dst.DirectedIn, src => src.Movies.Where(o => o.Type == MoviePersonType.Director));
        }
    }
}
