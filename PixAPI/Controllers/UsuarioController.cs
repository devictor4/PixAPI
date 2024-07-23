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
        public IActionResult ListarAtivos() =>
            Ok(new { usuarios = _usuarioService.ListarAtivos() });

        [HttpGet]
        [Route("{id}")]
        public IActionResult BuscarAtivoPeloId(long id) =>
            Ok(new { usuario = _usuarioService.BuscarAtivoPeloId(id) });

        [HttpPost]
        public IActionResult Cadastrar(
            [FromBody] CadastroModel model) =>
            Ok(new
            {
                usuario = _usuarioService.Cadastrar(model.TipoDocumento, model.Documento, 
                    model.Email, model.Senha, model.Nome, model.DDDCelular, model.Celular)
            });

        [HttpPut]
        public IActionResult Alterar(
            [FromBody] AlteracaoModel model) =>
            Ok(new
            {
                usuario = _usuarioService.Alterar(model.TipoDocumento, model.Documento,
                    model?.Email, model?.Nome,  model?.DDDCelular, model?.Celular)
            });

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DesativarPeloid(long id) =>
            Ok(new { usuario = _usuarioService.DesativarPeloId(id) } );
    }
}
