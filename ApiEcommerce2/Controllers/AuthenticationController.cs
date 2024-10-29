using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using ApiEcommerce2.DTOs;
using Domain.Interfaces;

namespace ApiEcommerce2.Controllers
{

    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUsuarioService _usuarioService;

        public AuthenticationController(IConfiguration config, IUsuarioService usuarioService)
        {
            _config = config;
            _usuarioService = usuarioService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate([FromBody] UsuarioLoginDTO loginDto)
        {
            // Paso 1: Validación de credenciales
            var token = await _usuarioService.LoginAsync(loginDto.Email, loginDto.Password);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }
}







































    ///public class AuthenticationController : Controller
    ///{
    ///[HttpPost]



    ///public IActionResult<string> Authenticate([FromBody] CredencialesRequest Credencialesre)
    ///{
    ///return Ok("JWT");
    ///}
    ///}

