using PixAPI.Repository.Entities;
using static PixAPI.Business.Util.Enumerators;

namespace PixAPI.Business.DTOs
{
    public class UsuarioDTO
    {
        public long ID { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public string Documento { get; set; }
        public short DDDCelular { get; set; }
        public long Celular { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public bool IsExcluido { get; set; }

        public UsuarioDTO()
        {
            
        }

        public UsuarioDTO(Usuario usuario)
        {
            ID = usuario.id;
            Email = usuario.email;
            Nome = usuario?.nome ?? "";
            TipoDocumento = (TipoDocumento)usuario.tipoDocumento;
            Documento = usuario.documento;
            DDDCelular = usuario.dddCelular;
            Celular = usuario.celular;
            DataInclusao = usuario.dataInclusao;
            DataAlteracao = usuario.dataAlteracao;
            DataExclusao = usuario.dataExclusao;
            IsExcluido = usuario.isExcluido;
        }
    }
}
