using Serilog;
using System.Net;

namespace CookingAPI.ErrorHandler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Se produjo un error no manejado: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                await context.Response.WriteAsync("Se produjo un error interno en el servidor.");
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                Log.Warning("Recurso no encontrado: {Path}", context.Request.Path);
                await context.Response.WriteAsync("Recurso no encontrado.");
            }
        }
    }

}
