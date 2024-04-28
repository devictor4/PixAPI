using System.ComponentModel.DataAnnotations;

namespace PixAPI.Repository.Entities
{
    public class UsuarioChavePix
    {
        public long id { get; set; }
        public long idUsuario { get; set; }
        public long idChavePix { get; set; }
    }
}
