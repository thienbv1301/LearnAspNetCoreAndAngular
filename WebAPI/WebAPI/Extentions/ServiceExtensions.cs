using Microsoft.AspNetCore.Builder;
using WebAPI.Middlewares;

namespace WebAPI.Extentions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
