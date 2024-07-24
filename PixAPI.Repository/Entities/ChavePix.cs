using System.ComponentModel.DataAnnotations;

namespace PixAPI.Repository.Entities
{
    public class ChavePix
    {
        [Key]
        public long id { get; set; }
        public long idTipo { get; set; }
        public string chave { get; set; }
        public DateTime dataInclusao { get; set; }
        public DateTime? dataAlteracao { get; set; }
        public DateTime? dataExclusao { get; set; }
        public bool isExcluido { get; set; }
    }
}
