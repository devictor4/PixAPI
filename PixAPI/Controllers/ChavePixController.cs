using Microsoft.AspNetCore.Mvc;
using PixAPI.Business.Models.ChavePix;
using PixAPI.Business.Services;

namespace PixAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChavePixController : ControllerBase
    {
        private readonly ChavePixService _chavePixService;

        public ChavePixController(ChavePixService chavePixService)
        {
            _chavePixService = chavePixService;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar(
            [FromBody] CadastarChaveModel model) =>
            Ok(_chavePixService.Cadastrar(model.UsuarioId, model.TipoChave, model.Chave));
    }
}
