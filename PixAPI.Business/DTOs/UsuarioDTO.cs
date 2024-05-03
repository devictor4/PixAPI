using PixAPI.Repository.Entities;
using static PixAPI.Business.Util.Enumerators;

namespace PixAPI.Business.DTOs
{
    public class UsuarioDTO
    {
        public string? Nome { get; set; }
        public TipoDocumento? TipoDocumento { get; set; }
        public long? Documento { get; set; }
        public string? Email { get; set; }
        public long? Telefone { get; set; }

        public UsuarioDTO()
        {
            
        }

        public UsuarioDTO(Usuario? usuario)
        {
            Nome = usuario?.nome ?? "";
            TipoDocumento = (TipoDocumento?)usuario?.tipoDocumento;
            Documento = usuario?.documento;
            Email = usuario?.email;
            Telefone = usuario?.telefone;
        }
    }
}
