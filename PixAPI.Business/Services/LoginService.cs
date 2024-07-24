using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PixAPI.Business.Exceptions;
using PixAPI.Repository.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixAPI.Business.Services
{
    public class LoginService
    {
        private readonly PixAPIContext _pixAPIContext;
        private readonly IConfiguration _configuration;

        public LoginService(PixAPIContext pixAPIContext,
            IConfiguration configuration)
        {
            _pixAPIContext = pixAPIContext;
            _configuration = configuration;
        }

        public string Login(string? email, string? documento, string senha)
        {
            if (string.IsNullOrWhiteSpace(email)
                && string.IsNullOrWhiteSpace(documento)) throw new BadRequestException("É necessário informar os dados de Login.");

            var usuario = _pixAPIContext.Usuario
                .FirstOrDefault(e => !e.isExcluido 
                    && (!string.IsNullOrWhiteSpace(email) && e.email.Equals(email)
                    || !string.IsNullOrWhiteSpace(documento) && e.documento.Equals(documento)))
                        ?? throw new BadRequestException($"E-mail ou Documento inválido.");

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.senha)) 
                throw new BadRequestException($"Senha inválida.");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}
