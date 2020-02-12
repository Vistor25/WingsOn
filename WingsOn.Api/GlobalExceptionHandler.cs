using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WingsOn.Bll;

namespace WingsOn.Api
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static  Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            if (exception.GetType() == typeof(EntityNotFoundException))
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
            else if (exception.GetType() == typeof(ArgumentException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return  context.Response.WriteAsync($"{context.Response.StatusCode}: {exception.Message}");
        }
    }
}
