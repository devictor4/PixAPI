using Microsoft.AspNetCore.Mvc;
using PixAPI.Business.Models.Usuario;
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
        [Route("Listar")]
        public IActionResult Listar() =>
            Ok(new { usuarios = _usuarioService.Listar() });

        [HttpGet]
        [Route("Buscar/{id}")]
        public IActionResult BuscarPeloId(long id) =>
            Ok(new { usuario = _usuarioService.BuscarPeloId(id) });

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(
            [FromBody] CadastrarUsuarioModel model) =>
            Ok(new
            {
                usuario = _usuarioService.Cadastrar(model.TipoDocumento, model.Documento, 
                    model.Email, model.Senha, model.Nome, model.DDDCelular, model.Celular)
            });

        [HttpPut]
        [Route("Alterar")]
        public IActionResult Alterar(
            [FromBody] AlterarUsuarioModel model) =>
            Ok(new
            {
                usuario = _usuarioService.Alterar(model.TipoDocumento, model.Documento,
                    model?.Email, model?.Nome, model?.DDDCelular, model?.Celular)
            });

        [HttpDelete]
        [Route("Desativar/{id}")]
        public IActionResult DesativarPeloid(long id) =>
            Ok(new { usuario = _usuarioService.DesativarPeloId(id) } );
    }
}
