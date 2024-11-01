using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.Service;


namespace ApiEcommerce2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Policy = "Admin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtTokenService _jwtTokenService;

        public UsuarioController(IUsuarioService usuarioService, IJwtTokenService jwtTokenService)
        {
            _usuarioService = usuarioService;
            _jwtTokenService = jwtTokenService;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsuarios()
        {
            var user = User.Identity as ClaimsIdentity;
            var roleClaim = user?.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim == null || roleClaim != "Admin")
            {
                return Unauthorized(new { message = "Credenciales inválidas" }); 
            }

            var usuarios = await _usuarioService.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<IActionResult> GetUsuario(string email)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorEmail(email);
            if (usuario == null) return NotFound(new { message = "Credenciales inválidas" });
            return Ok(usuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Password))
            {
                return BadRequest("El nombre, el email y la contraseña del usuario no pueden ser nulos.");
            }

            // Verificar si el usuario ya existe por su email
            var usuarioExistente = await _usuarioService.ObtenerUsuarioPorEmail(usuario.Email);
            if (usuarioExistente != null)
            {
                return Conflict(new { mensaje = "Ya existe un usuario con este email." });
            }

            if (usuario.Role != "Admin" && usuario.Role != "Vendedor")
            {
                usuario.Role = "Cliente";
            }

            // Realiza hash de la contraseña
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            await _usuarioService.AgregarUsuario(usuario);

            // Generar token JWT para el nuevo usuario
            var token = _jwtTokenService.GenerateToken(usuario);

            return Ok(new { mensaje = "Usuario cliente creado correctamente.", token });
        }

        [HttpPut("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ModificarUsuario(string email, [FromBody] Usuario usuario)
        {
            var existingUsuario = await _usuarioService.ObtenerUsuarioPorEmail(email);
            if (existingUsuario == null)
            {
                return NotFound();
            }

            // Verificar si el nuevo email ya está en uso por otro usuario
            if (usuario.Email != null && usuario.Email != existingUsuario.Email)
            {
                var usuarioConNuevoEmail = await _usuarioService.ObtenerPorEmailAsync(usuario.Email);
                if (usuarioConNuevoEmail != null)
                {
                    return Conflict(new { mensaje = "El email ya está en uso por otro usuario." });
                }
            }

            // Actualizar propiedades del usuario existente
            existingUsuario.Nombre = usuario.Nombre ?? existingUsuario.Nombre;
            existingUsuario.Email = usuario.Email ?? existingUsuario.Email;
            existingUsuario.Role = string.IsNullOrEmpty(usuario.Role) ? existingUsuario.Role : usuario.Role;

            await _usuarioService.ModificarUsuario(existingUsuario);
            return NoContent();
        }

        [HttpDelete("{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EliminarUsuario(string email)
        {
            var user = User.Identity as ClaimsIdentity;
            var roleClaim = user?.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim == null || roleClaim != "Admin")
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var usuario = await _usuarioService.ObtenerUsuarioPorEmail(email);
            if (usuario == null) return NotFound();

            await _usuarioService.EliminarUsuarioPorEmail(email);
            return NoContent();
        }
    }
}
