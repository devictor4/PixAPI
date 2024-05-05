namespace PixAPI.Business.Models
{
    public class CadastrarOuAtualizarModel : DocumentoModel
    {
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public long? Telefone { get; set; }
    }
}
