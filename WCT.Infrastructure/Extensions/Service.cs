using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WCT.Infrastructure.DBContexts;

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
                   .WithOrigins(configuration
                      .GetSection("AllowedHosts")
                      .Get<string[]>());
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
    }
}