using jwt_example.Models.User;
using jwt_example.Repositories.User;
using jwt_example.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public HomeController(ITokenService tokenService, IUserRepository userRepository, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] UserModel model)
        {
            
            var user = _userRepository.GetUser(model);
                        
            if (user == null)
                return NotFound(new { message = "Invalid Username or Password" });
                        
            var token = _tokenService.BuildToken(_configuration["Jwt:Key"] ,user);                        
                        
            return new
            {
                user = user.UserName,
                token = token
            };
        }

        [Authorize]
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
