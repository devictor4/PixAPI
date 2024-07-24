using Microsoft.IdentityModel.Tokens;
using PixAPI.Business.DTOs.Login;
using PixAPI.Business.Exceptions;
using PixAPI.Business.Util;
using PixAPI.Repository.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PixAPI.Business.Services
{
    public class LoginService
    {
        private readonly PixAPIContext _pixAPIContext;

        public LoginService(PixAPIContext pixAPIContext)
        {
            _pixAPIContext = pixAPIContext;
        }

        public TokenAuthDTO Login(string? email, string? documento, string senha)
        {
            if (string.IsNullOrWhiteSpace(email)
                && string.IsNullOrWhiteSpace(documento)) throw new BadRequestException("É necessário informar os dados de Login.");

            var usuario = _pixAPIContext.Usuario
                .FirstOrDefault(e => !e.isExcluido 
                    && (!string.IsNullOrWhiteSpace(email) && e.email.Equals(email)
                    || !string.IsNullOrWhiteSpace(documento) && e.documento.Equals(documento)))
                ?? throw new UnauthorizedException("E-mail ou Documento inválido.");

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.senha)) 
                throw new UnauthorizedException("Senha inválida.");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.SECRET));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenExpires = DateTime.Now.AddMinutes(10);

            var tokenOptions = new JwtSecurityToken(
                issuer: Settings.ISSUER,
                audience: Settings.AUDIENCE,
                claims: new List<Claim>(),
                expires: tokenExpires,
                signingCredentials: signinCredentials
            );

            return new TokenAuthDTO()
            {
                Criacao = DateTime.Now,
                Expiracao = tokenExpires,
                Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
            };
        }
    }
}
