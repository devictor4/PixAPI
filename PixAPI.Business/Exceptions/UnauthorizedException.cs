using System.Net;

namespace PixAPI.Business.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string? message = null,
            object? request = null, object? response = null)
            : base(message, HttpStatusCode.Unauthorized,
                  request, response)
        {
        }
    }
}
