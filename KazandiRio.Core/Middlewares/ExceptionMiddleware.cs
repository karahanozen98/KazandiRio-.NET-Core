using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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
            catch(Exception e)
            {
                Console.WriteLine(e);
                httpContext.Response.StatusCode = 400;
                var json = JsonConvert.SerializeObject(e.Message);
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
