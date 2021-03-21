using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Net;

namespace WCT.Infrastructure.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null && contextFeature.Error != null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        Log.Error(contextFeature.Error, "Something went wrong.");

                        await context.Response.WriteAsync(JObject.FromObject
                            (new { statuscode = context.Response.StatusCode, message = "Internal Server Error." }).ToString());
                    }
                });
            });
        }
    }
}