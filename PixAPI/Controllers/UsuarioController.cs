using Microsoft.AspNetCore.Mvc;
using PixAPI.Business.Exceptions;
using PixAPI.Business.Services;

namespace PixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Listar() =>
            Ok(new { usuarios = _usuarioService.Listar() });
    }
}
