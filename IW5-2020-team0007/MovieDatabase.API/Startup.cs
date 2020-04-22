using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieDatabase.API.Services;
using NJsonSchema.Generation;
using MovieDatabase.Data;
using Microsoft.AspNetCore.HttpOverrides;
using MovieDatabase.Data.MappingProfiles;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using MovieDatabase.API.Models.Auth;

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
            services.Configure<AuthSettings>(Configuration.GetSection("Auth"));

            // Logger
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

            // Database and mapping
            services
                .AddDatabase(Configuration.GetConnectionString("Default"))
                .AddAutoMapping();

            services
                .AddCors()
                .AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.Configure<ForwardedHeadersOptions>(opt =>
            {
                opt.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            services
                .AddOpenApiDocument(settings =>
                {
                    settings.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;
                });

            services
                .AddScoped<UsersService>()
                .AddTransient<AuthService>();
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
                    };
                })
                .UseSwaggerUi3()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
