using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services
                .AddOpenApiDocument(settings =>
                {
                    settings.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;
                    
                    settings.PostProcess = doc =>
                    {
                        doc.Info.Title = "MovieDatabase API";
                        doc.Info.Version = "v1";
                        doc.Info.Contact = new NSwag.OpenApiContact()
                        {
                            Name = "Michal Halabica, Jakub Koudelka, Konupèík Viktor"
                        };
                    };
                })
                .AddControllers();
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
                .UseOpenApi()
                .UseSwaggerUi3()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
