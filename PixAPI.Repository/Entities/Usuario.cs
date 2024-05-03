using System.ComponentModel.DataAnnotations;

namespace PixAPI.Repository.Entities
{
    public class Usuario
    {
        [Key]
        public long id { get; set; }
        public string nome { get; set; }
        public byte tipoDocumento { get; set; }
        public long documento { get; set; }
        public string? email { get; set; }
        public long? telefone { get; set; }
        public DateTime dataInclusao { get; set; }
        public DateTime? dataAlteracao { get; set; }
        public DateTime? dataExclusao { get; set; }
        public bool isExcluido { get; set; }
    }
}
