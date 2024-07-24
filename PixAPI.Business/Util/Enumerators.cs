using System.ComponentModel;

namespace PixAPI.Business.Util
{
    public class Enumerators
    {
        public enum TipoDocumento
        {
            CPF = 1,
            CNPJ = 2
        }

        public enum TipoChave
        {
            [Description("CPF")]
            CPF = 1,
            [Description("CNPJ")]
            CNPJ = 2,
            [Description("E-mail")]
            Email = 3,
            [Description("Chave Aleatória")]
            ChaveAleatoria = 4
        }
    }
}
