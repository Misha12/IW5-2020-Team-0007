using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieDatabase.API.Services;
using MovieDatabase.Domain;
using NJsonSchema.Generation;
using AutoMapper;

namespace MovieDatabase.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder
                    .SetMinimumLevel(LogLevel.Information)
                    .AddConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.TimestampFormat = "[dd. MM. yyyy HH:mm:ss]\t";    
                    });
            });

            services.AddCors();

            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<MovieDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddAutoMapper(typeof(Startup));
            services
                .AddOpenApiDocument(settings =>
                {
                    settings.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;
                })
                .AddControllers();

            services
                .AddScoped<GenreService>()
                .AddScoped<MovieService>()
                .AddScoped<PersonService>()
                .AddScoped<RatesService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseStaticFiles()
                .UseOpenApi(settings =>
                {
                    settings.PostProcess = (doc, _) =>
                    {
                        doc.Info.Title = "MovieDatabase API";
                        doc.Info.Version = "v1";
                        doc.Info.Contact = new NSwag.OpenApiContact()
                        {
                            Name = "Michal Halabica, Jakub Koudelka, Konupèík Viktor",
                            Url = ""
                        };

                        doc.Servers.Add(new NSwag.OpenApiServer() { Url = "http://zakladna.eu:60001", Description = "Production" });
                    };
                })
                .UseSwaggerUi3()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
