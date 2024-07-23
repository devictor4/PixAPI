//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using PixAPI.Business.DTOs;
//using PixAPI.Business.Exceptions;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace PixAPI.Business.Services
//{
//    public class AuthService
//    {
//        private readonly IConfiguration _configuration;

//        public AuthService(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public string Auth(AuthDTO loginDTO)
//        {
//            if (loginDTO is null) throw new BadRequestException("É necessário informar os dados de Login.");

//            //Consulta SQL à partir do LOGIN e SENHA

//            //Condição pra só gerar o token se o login e senha bater.
//            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
//            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//            var tokeOptions = new JwtSecurityToken(
//                issuer: _configuration["JWT:Issuer"],
//                audience: _configuration["JWT:Audience"],
//                claims: new List<Claim>(),
//                expires: DateTime.Now.AddMinutes(10),
//                signingCredentials: signinCredentials
//            );

//            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
//        }
//    }
//}
