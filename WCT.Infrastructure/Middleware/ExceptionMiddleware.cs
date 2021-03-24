using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
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
                        var errorDetails = GetErrorCode(contextFeature.Error);
                        context.Response.StatusCode = (int)errorDetails.Item1;
                        context.Response.ContentType = "application/json";

                        Log.Error(contextFeature.Error, "Something went wrong.");

                        await context.Response.WriteAsync(JObject.FromObject
                            (new { statuscode = context.Response.StatusCode, message = errorDetails.Item2 }).ToString());
                    }
                });
            });
        }

        private static (HttpStatusCode, string) GetErrorCode(Exception e)
        {
            switch (e)
            {
                case DbUpdateException ex:
                    return (HttpStatusCode.BadRequest, ex.InnerException?.Message);

                default:
                    return (HttpStatusCode.InternalServerError, "Internal Server Error.");
            }
        }
    }
}