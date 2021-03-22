using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Text;
using WCT.Infrastructure.DBContexts;
using WCT.Infrastructure.Extensions;
using WCT.Infrastructure.Middleware;

namespace WCT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,

                ValidIssuer = this.Configuration
                .GetValue<string>("JwtSettings:validIssuer"),
                ValidAudience = this.Configuration
                .GetValue<string>("JwtSettings:validAudience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(this.Configuration
                .GetValue<string>("JwtSettings:secretKey")))
            };
        }

        public IConfiguration Configuration { get; }
        public TokenValidationParameters TokenValidationParameters { get; }

        // This method gets called by the runtime. Use this method to add
        // services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(this.Configuration);
            services.ConfigureSwagger();
            services.ConfigureDbContext(this.Configuration);
            services.ConfigureIdentity();
            services.ConfigureAuthentication(this.TokenValidationParameters);
            services.ConfigureRepositoryManager();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure
        // the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            DBContext context)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Error migrating database.");
                throw;
            }

            app.ConfigureExceptionHandler();

            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}