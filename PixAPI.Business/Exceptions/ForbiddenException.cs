using System.Net;

namespace PixAPI.Business.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException(string? message = null,
            object? request = null, object? response = null)
            : base(message, HttpStatusCode.Forbidden,
                  request, response)
        {
        }
    }
}
