using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using ApiEcommerce2.DTOs;
using Domain.Interfaces;
using Domain.Entities;

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

        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Register([FromBody] UsuarioRegistroDTO registroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el usuario ya existe
            var usuarioExistente = await _usuarioService.ObtenerPorEmailAsync(registroDto.Email);
            if (usuarioExistente != null)
            {
                return Conflict(new { mensaje = "Ya existe un usuario con este email." });
            }

            // Asignar rol Cliente por defecto si no es Vendedor o Admin
            string rolAsignado = (registroDto.Role == "Vendedor" || registroDto.Role == "Admin") ? registroDto.Role : "Cliente";

            var nuevoUsuario = await _usuarioService.RegisterAsync(
                registroDto.Nombre,
                registroDto.Apellido,
                registroDto.Email,
                registroDto.Password,
                rolAsignado, // Usar el rol asignado
                registroDto.IsAdminLoggedIn // Para lógica de registro
            );

            return CreatedAtAction(nameof(Register), nuevoUsuario);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate([FromBody] UsuarioLoginDTO loginDto)
        {
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

