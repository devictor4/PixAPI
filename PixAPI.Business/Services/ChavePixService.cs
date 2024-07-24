using PixAPI.Business.DTOs.PIX;
using PixAPI.Business.DTOs.Usuarios;
using PixAPI.Business.Utils;
using PixAPI.Repository.Context;
using PixAPI.Repository.Entities;
using static PixAPI.Business.Util.Enumerators;

namespace PixAPI.Business.Services
{
    public class ChavePixService
    {
        private readonly PixAPIContext _pixAPIContext;
        private readonly UsuarioService _usuarioService;

        public ChavePixService(PixAPIContext pixAPIContext,
            UsuarioService usuarioService)
        {
            _pixAPIContext = pixAPIContext;
            _usuarioService = usuarioService;
        }

        public ChavePixDTO Cadastrar(long idUsuario, TipoChave tipoChavePix, string chave) 
        {
            UsuarioDTO? usuarioDTO = _usuarioService.BuscarPeloId(idUsuario);

            ChavePix chavePix = new()
            {
                chave = chave,
                idTipo = tipoChavePix.GetHashCode(),
                dataInclusao = DateTime.Now,
                isExcluido = false,
            };

            _pixAPIContext.ChavePix.Add(chavePix);
            _pixAPIContext.SaveChanges();

            UsuarioChavePix usuarioChavePix = new()
            {
                idChavePix = chavePix.id,
                idUsuario = idUsuario 
            };

            _pixAPIContext.UsuarioChavePix.Add(usuarioChavePix);
            _pixAPIContext.SaveChanges();

            return new ChavePixDTO()
            {
                Nome = usuarioDTO?.Nome,
                Documento = usuarioDTO?.Documento,
                Tipo = Utilities.GetEnumDescription(tipoChavePix),
                Chave = chave
            };
        }
    }
}
