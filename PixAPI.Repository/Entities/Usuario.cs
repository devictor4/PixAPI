using System.ComponentModel.DataAnnotations;

namespace PixAPI.Repository.Entities
{
    public class Usuario
    {
        [Key]
        public long id { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public int tipoDocumento { get; set; }
        public string documento { get; set; }
        public short dddCelular { get; set; }
        public long celular { get; set; }
        public DateTime dataInclusao { get; set; }
        public DateTime? dataAlteracao { get; set; }
        public DateTime? dataExclusao { get; set; }
        public bool isExcluido { get; set; }
    }
}
