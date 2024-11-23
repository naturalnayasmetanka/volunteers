using Volunteers.API.Middlewares;

namespace Volunteers.API.Extentions.Middlewares;

public static class ExceptionMiddlewareExtentions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}