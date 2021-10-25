using jwt_example.Models.User;
using jwt_example.Repositories.User;
using jwt_example.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt_example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
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

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = _tokenService.BuildToken(_configuration["Jwt:Key"] ,user);                        

            // Retorna os dados
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
