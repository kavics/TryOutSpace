using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MidWarez.ClassLibrary1
{
    public static class ResponseManipulatorExtensions
    {
        public static IApplicationBuilder UseResponseManipulator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseManipulator>();
        }
    }
    public class ResponseManipulator
    {
        private RequestDelegate _next;

        public ResponseManipulator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var newContent = string.Empty;
            var originalBody = httpContext.Response.Body;
            using (var newBody = new MemoryStream())
            {
                httpContext.Response.Body = newBody;
                await _next(httpContext);
                httpContext.Response.Body = originalBody;

                newBody.Seek(0, SeekOrigin.Begin);
                newContent = new StreamReader(newBody).ReadToEnd();
                newContent += $"\nMiddleware called: { this.GetType().FullName}";

                await httpContext.Response.WriteAsync(newContent);
            }

        }
    }
}
