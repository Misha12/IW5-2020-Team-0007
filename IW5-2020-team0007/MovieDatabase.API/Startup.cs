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
using System.Text.Json.Serialization;
using MovieDatabase.API.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag;
using NSwag.Generation.Processors.Security;
using MovieDatabase.API.Models.Email;

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
                .Configure<AuthSettings>(Configuration.GetSection("Auth"))
                .Configure<EmailSettings>(Configuration.GetSection("Email"))
                .Configure<ForwardedHeadersOptions>(opt =>
                {
                    opt.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                });

            // Database, mapping, logger
            services
                .AddDatabase(Configuration.GetConnectionString("Default"))
                .AddAutoMapping()
                .AddLogging(builder =>
                {
                    builder
                        .SetMinimumLevel(LogLevel.Information)
                        .AddConsole(options =>
                        {
                            options.IncludeScopes = true;
                            options.TimestampFormat = "[dd. MM. yyyy HH:mm:ss]\t";
                        });
                })
                .AddAuthentication(builder =>
                {
                    builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(builder =>
                {
                    builder.RequireHttpsMetadata = false;
                    builder.SaveToken = true;
                    builder.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Auth")["Secret"])),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = Configuration.GetSection("Auth")["Issuer"]
                    };
                });

            services
                .AddCors()
                .AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services
                .AddOpenApiDocument(settings =>
                {
                    settings.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;

                    settings.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = OpenApiSecurityApiKeyLocation.Header,
                        Type = OpenApiSecuritySchemeType.ApiKey
                    });

                    settings.OperationProcessors.Add(new OperationSecurityScopeProcessor());
                });

            services
                .AddScoped<GenreService>()
                .AddScoped<MailService>()
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
                    settings.PostProcess = (doc, request) =>
                    {
                        doc.Info.Title = "MovieDatabase API";
                        doc.Info.Version = "v1";
                        doc.Info.Contact = new NSwag.OpenApiContact()
                        {
                            Name = "Michal Halabica, Jakub Koudelka, Konupèík Viktor",
                            Url = "https://dev.azure.com/iw5-2020-team0007/project"
                        };

                        doc.Servers.Add(new OpenApiServer() 
                        { 
                            Url = "https://iw5.zakladna.eu",
                            Description = "Production"
                        });
                    };
                })
                .UseSwaggerUi3()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
