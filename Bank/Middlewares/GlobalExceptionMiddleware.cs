using Bank.Exceptions;
using Serilog;
using System.Net;

namespace Bank.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
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
                Log.Error(ex, "An unexpected error occurred");          
                await HandleExceptionAsunc(context, ex);
                
            }
        }
        private static Task HandleExceptionAsunc(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode=exception switch
            {
                AccountNotFoundException _ => HttpStatusCode.NotFound,
                CustomException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = (int)statusCode;

            var result = new 
            { 
                errorMessage = exception.Message,

            };
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
