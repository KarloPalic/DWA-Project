using DAL.DataTransferObject;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DWA_Project.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDTO userRegistrationDTO)
        {
            if (await _userRepository.RegisterUser(userRegistrationDTO))
            {
                return Ok("User registered successfully. Check your email for confirmation.");
            }

            return BadRequest("User registration failed.");
        }
    }
}
