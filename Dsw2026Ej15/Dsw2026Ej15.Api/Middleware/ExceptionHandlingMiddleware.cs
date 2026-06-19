using System.Runtime.CompilerServices;

namespace Dsw2026Ej15.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context) {
            try
            {
                await _next(context);

            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var errorResponse = new { error = ex.Message };
                context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch (Exception ex)
            {
               
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var errorResponse = new { error = "Ocurrió un error inesperado" };
                context.Response.WriteAsJsonAsync(errorResponse);
            }
            catch 
            { 
                await HandleExceptionAsync(context,ex);
            }

            private async Task HandleExceptionAsync 
}
