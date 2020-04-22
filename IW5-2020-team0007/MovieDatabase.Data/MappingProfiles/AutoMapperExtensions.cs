using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MovieDatabase.Data.MappingProfiles
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(GenreMappingProfile),
                typeof(MovieMappingProfile),
                typeof(PersonMappingProfile),
                typeof(RatingMappingProfile),
                typeof(SearchMappingProfile),
                typeof(UserMappingProfile)
            );

            return services;
        }
    }
}
