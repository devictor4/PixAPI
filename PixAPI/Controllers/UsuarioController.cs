using Microsoft.AspNetCore.Mvc;
using PixAPI.Business.DTOs;
using PixAPI.Business.Exceptions;
using PixAPI.Business.Models;
using PixAPI.Business.Services;
using static PixAPI.Business.Util.Enumerators;

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
        public IActionResult ListarTodosAtivos() =>
            Ok(new { usuarios = _usuarioService.ListarTodosAtivos() });

        [HttpGet]
        [Route("{tipoDocumento}/{documento}")]
        public IActionResult BuscarAtivoPeloDocumento(TipoDocumento tipoDocumento, long documento) =>
            Ok(new { usuario = _usuarioService.BuscarAtivoPeloDocumento(tipoDocumento, documento) });

        [HttpPost]
        public IActionResult CadastrarOuAtualizar(
            [FromBody] CadastrarOuAtualizarModel model) =>
            Ok(new
            {
                usuario = _usuarioService.CadastrarOuAtualizar(
                    model.TipoDocumento, model.Documento, model.Nome, model.Telefone, model.Email)
            });

        [HttpDelete]
        [Route("{tipoDocumento}/{documento}")]
        public IActionResult DesativarPeloDocumento(TipoDocumento tipoDocumento, long documento) =>
            Ok(new { usuario = _usuarioService.DesativarPeloDocumento(tipoDocumento, documento) } );
    }
}
