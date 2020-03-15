using AutoMapper;

namespace MovieDatabase.API.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entity.Person, Domain.DTO.Person>();
            CreateMap<Domain.Entity.Person, Domain.DTO.PersonDetail>();
            CreateMap<Domain.Entity.Movie, Domain.DTO.Movie>();
        }
    }
}