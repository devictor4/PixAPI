using PixAPI.Business.Exceptions;
using System.Net;
using System.Net.Http.Headers;

namespace PixAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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
                await UseApiExceptionHandler(context, ex);
            }
        }

        public async Task<HttpContext> UseApiExceptionHandler(
            HttpContext context, Exception exception)
        {
            dynamic? exceptionError = GetError(exception);

            HttpStatusCode statusCode = (HttpStatusCode)(exceptionError?.StatusCode
                 ?? HttpStatusCode.InternalServerError);
            string? error = exceptionError?.Error;

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = (int)statusCode,
                error = statusCode.ToString(),
                message = error,
                stackTrace = statusCode == HttpStatusCode.InternalServerError
                        ? exceptionError?.StackTrace : null
            });

            return context;
        }

        private dynamic? GetError(Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            AppException? appException = null;
            string? error = exception?.Message;

            if (exception != null && exception is AppException)
            {
                appException = exception as AppException;

                statusCode = appException.StatusCode;
                if (statusCode != HttpStatusCode.InternalServerError)
                    error = appException.UserMessage;
            }

            return new { StatusCode = statusCode, Error = error, exception?.StackTrace };
        }
    }
}
