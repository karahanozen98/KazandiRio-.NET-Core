using KazandiRio.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KazandiRio.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case NotFoundException ex:
                        await WriteException(httpContext, ex.Message, 404);
                        break;
                    default:
                        await WriteException(httpContext, e.Message, 400);
                        break;

                }

            }
        }
        private async Task WriteException(HttpContext httpContext, string message, int statusCode)
        {
            httpContext.Response.StatusCode = 400;
            var json = JsonConvert.SerializeObject(message);
            await httpContext.Response.WriteAsync(json);
        }
    }
}
