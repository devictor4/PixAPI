using PixAPI.Repository.Context;

namespace PixAPI.Business.Services
{
    public class LoginService
    {
        private readonly PixAPIContext _pixAPIContext;

        public LoginService(PixAPIContext pixAPIContext)
        {
            _pixAPIContext = pixAPIContext;
        }
    }
}
