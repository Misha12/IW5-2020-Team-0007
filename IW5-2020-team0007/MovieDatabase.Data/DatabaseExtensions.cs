using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieDatabase.Data.Repository;

namespace MovieDatabase.Data
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(builder => builder.UseSqlServer(connectionString));

            services
                .AddScoped<GenresRepository>()
                .AddScoped<PersonsRepository>();

            return services;
        }
    }
}
