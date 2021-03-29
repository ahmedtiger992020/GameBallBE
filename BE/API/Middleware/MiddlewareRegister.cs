using Microsoft.AspNetCore.Builder;

namespace GB.API
{
    public static class MiddlewareRegister
    {
        public static void UseAppMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
