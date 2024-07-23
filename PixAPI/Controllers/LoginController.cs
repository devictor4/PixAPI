using Microsoft.AspNetCore.Mvc;
using PixAPI.Business.Services;

namespace PixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public LoginController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
    }
}
