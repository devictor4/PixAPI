using Newtonsoft.Json;
using System.Net;

namespace PixAPI.Business.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public override string Message => UserMessage ?? base.Message;
        public string ErrorMessage => base.Message;
        public string? UserMessage { get; }
        public string? Request { get; set; }
        public string? Response { get; set; }

        public AppException(string? message = null,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            dynamic? request = null, dynamic? response = null) : base()
        {
            StatusCode = statusCode;
            UserMessage = message;
            Request = request != null ? JsonConvert.SerializeObject(request) : null;
            Response = response != null ? JsonConvert.SerializeObject(response) : null;
        }

        public static void Throw(string? message = null) =>
            ThrowByStatusCode(message);

        public static void ThrowByStatusCode(string? message = null,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            dynamic? request = null, dynamic? response = null) =>
            throw statusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException(message, request, response),
                HttpStatusCode.Forbidden => new ForbiddenException(message, request, response),
                HttpStatusCode.BadRequest => new BadRequestException(message, request, response),
                HttpStatusCode.Unauthorized => new UnauthorizedException(message, request, response),
                _ => new AppException(message, statusCode, request, response),
            };
    }
}
