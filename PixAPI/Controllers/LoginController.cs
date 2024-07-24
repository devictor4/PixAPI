using Microsoft.AspNetCore.Mvc;
using PixAPI.Business.Models.Login;
using PixAPI.Business.Services;

namespace PixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login(
            [FromBody] LoginModel model) =>
            Ok(_loginService.Login(model.Email, model.Documento, model.Senha));
    }
}
