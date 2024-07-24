using static PixAPI.Business.Util.Enumerators;

namespace PixAPI.Business.Models.ChavePix
{
    public class CadastarChaveModel
    {
        public long UsuarioId { get; set; }
        public TipoChave TipoChave { get; set; }
        public string Chave { get; set; }
    }
}
