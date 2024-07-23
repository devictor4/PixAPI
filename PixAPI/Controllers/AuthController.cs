//using Microsoft.AspNetCore.Mvc;
//using PixAPI.Business.DTOs;
//using PixAPI.Business.Services;

//namespace PixAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly AuthService _authService;

//        public AuthController(AuthService authService)
//        {
//            _authService = authService;
//        }

//        public IActionResult Auth(
//            [FromBody] AuthDTO loginDTO) =>
//            Ok(new
//            {
//                Token = _authService.Auth(loginDTO)
//            });
//    }
//}
