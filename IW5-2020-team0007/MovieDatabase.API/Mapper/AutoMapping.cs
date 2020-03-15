using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace MovieDatabase.API.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entity.Person, Domain.DTO.Person>();
        }
    }
}