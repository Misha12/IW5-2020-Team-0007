using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieDatabase.API.Services;
using MovieDatabase.Domain;
using NJsonSchema.Generation;

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
            var connectionString = Configuration.GetConnectionString("Default");

            services.AddDbContext<MovieDatabaseContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services
                .AddOpenApiDocument(settings =>
                {
                    settings.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;
                })
                .AddControllers();

            services
                .AddScoped<GenreManagementService>()
                .AddScoped<MovieService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseAuthorization()
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
