﻿namespace PixAPI.Business.Models.Usuario
{
    public class AlteracaoModel : DocumentoModel
    {
        public string? Email { get; set; }
        public string? Nome { get; set; }
        public short? DDDCelular { get; set; }
        public long? Celular { get; set; }
    }
}
