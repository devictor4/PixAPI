using System.ComponentModel.DataAnnotations;

namespace PixAPI.Repository.Entities
{
    public class ChavePix
    {
        [Key]
        public long id { get; set; }
        public string chave { get; set; }
    }
}
