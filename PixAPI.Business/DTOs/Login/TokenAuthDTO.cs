namespace PixAPI.Business.DTOs.Login
{
    public class TokenAuthDTO
    {
        public DateTime Criacao { get; set; }
        public DateTime Expiracao { get; set; }
        public string Token { get; set; }

        public TokenAuthDTO()
        {
            
        }
    }
}
