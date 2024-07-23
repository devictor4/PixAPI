using System.ComponentModel.DataAnnotations;

namespace PixAPI.Repository.Entities
{
    public class TipoChavePix
    {
        [Key]
        public long id { get; set; }
        public string tipo { get; set; }
    }
}
