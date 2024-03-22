using DAL.DataTransferObject;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DWA_Project.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
       
            private readonly IUserRepository _userRepository;
            private readonly ITokenService _tokenService;

            public AuthenticationController(IUserRepository userRepository, ITokenService tokenService)
            {
                _userRepository = userRepository;
                _tokenService = tokenService;
            }

            [HttpPost("login")]
            public IActionResult Login(LoginDTO loginDTO)
            {
                var user = _userRepository.GetUserByUsername(loginDTO.Username);

                if (user == null || !VerifyPassword(loginDTO.Password, user.PwdHash, user.PwdSalt))
                {
                    return Unauthorized();
                }

                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            private bool VerifyPassword(string enteredPassword, string hashedPassword, string salt)
            {
                var hashedEnteredPassword = BCrypt.Net.BCrypt.HashPassword(enteredPassword, salt);

                return hashedEnteredPassword == hashedPassword;
            }
    }
}
