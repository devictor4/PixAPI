using System.Net;

namespace PixAPI.Business.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string? message = null,
            object? request = null, object? response = null)
            : base(message, HttpStatusCode.BadRequest,
                  request, response)
        {
        }
    }
}
