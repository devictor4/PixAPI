using PixAPI.Business.DTOs.Usuarios;

namespace PixAPI.Business.DTOs.PIX
{
    public class ChavePixDTO
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Tipo { get; set; }
        public string Chave { get; set; }
        
        public ChavePixDTO()
        {
            
        }
    }
}
