using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using WCT.Core;
using WCT.Infrastructure.DBContexts;
using WCT.Infrastructure.Filters;
using WCT.Infrastructure.Interfaces;
using WCT.Infrastructure.Repositories;

namespace WCT.Infrastructure.Extensions
{
    public static class Service
    {
        public static void ConfigureCors(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
               {
                   builder.AllowAnyHeader()
                   .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                   .AllowCredentials()
                   .SetIsOriginAllowed((host) => configuration
                      .GetSection("AllowedHosts")
                      .Get<string[]>().Any());
               });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WCT API",
                    Version = "v1"
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string []{}
                    }
                });
            });
        }

        public static void ConfigureDbContext(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DBContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                    o => o.MigrationsAssembly("WCT.Infrastructure"));
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = false;
            })
            .AddEntityFrameworkStores<DBContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureAuthentication(this IServiceCollection services,
           TokenValidationParameters tokenValidationParameters)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureValidationFilter(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilter>();
        }
    }
}