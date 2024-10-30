using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace ApiEcommerce2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Policy = "Admin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;


        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Obtener todos los usuarios
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsuarios()
        {
            var user = User.Identity as ClaimsIdentity;
            var roleClaim = user?.FindFirst(ClaimTypes.Role)?.Value;
            if (roleClaim == null || roleClaim != "Admin")
            {
                return Unauthorized(); // Devuelve un 401 si no hay rol o si el rol no es "Admin"
            }

            var usuarios = await _usuarioService.ObtenerUsuarios();
            return Ok(usuarios);
        }

        // Obtener un usuario por nombre
        [HttpGet("{nombre}")]
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<IActionResult> GetUsuario(string nombre)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorNombre(nombre);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // Crear un nuevo usuario
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Email))
            {
                return BadRequest("El nombre y el email del usuario no pueden ser nulos.");
            }

            // Si el rol no es Admin, se asigna Cliente automáticamente
            if (usuario.Role != "Admin" && usuario.Role != "Vendedor")
            {
                usuario.Role = "Cliente";
            }

            await _usuarioService.AgregarUsuario(usuario);
            return Ok("Usuario cliente creado correctamente.");
        }

        // Modificar un usuario existente por nombre
        [HttpPut("{nombre}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ModificarUsuario(string nombre, [FromBody] Usuario usuario)
        {
            var existingUsuario = await _usuarioService.ObtenerUsuarioPorNombre(nombre);
            if (existingUsuario == null) return NotFound();

            // Actualizar propiedades del usuario existente
            existingUsuario.Email = usuario.Email ?? existingUsuario.Email;
            existingUsuario.Role = string.IsNullOrEmpty(usuario.Role) ? "Cliente" : usuario.Role;

            await _usuarioService.ModificarUsuario(existingUsuario);
            return NoContent();
        }

        // Eliminar un usuario por nombre
        [HttpDelete("{nombre}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EliminarUsuario(string nombre)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorNombre(nombre);
            if (usuario == null) return NotFound();
            await _usuarioService.EliminarUsuarioPorNombre(nombre);
            return NoContent();
        }
    }
}
