using System.Net;

namespace PixAPI.Business.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string? message = null,
            object? request = null, object? response = null)
            : base(message, HttpStatusCode.NotFound,
                  request, response)
        {
        }
    }
}
