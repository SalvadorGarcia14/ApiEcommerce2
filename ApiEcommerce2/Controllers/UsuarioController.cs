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
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUsuarios()
        {
            var user = User.Identity as ClaimsIdentity;
            var roleClaim = user?.FindFirst(ClaimTypes.Role)?.Value; // Obtener el rol del usuario

            if (roleClaim == null)
            {
                return Unauthorized(); // Devuelve un 401 si no hay rol
            }

            var usuarios = await _usuarioService.ObtenerUsuarios();
            return Ok(usuarios);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
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
            return Ok("Usuario liente creado correctamente.");
        }

        // Modificar un usuario existente
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ModificarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario.Id != id)
            {
                return BadRequest("El ID del usuario en el cuerpo debe coincidir con el ID de la URL.");
            }

            var existingUsuario = await _usuarioService.ObtenerUsuarioPorId(id);
            if (existingUsuario == null) return NotFound();

            // Si el rango no está presente, se asigna 'Cliente' por defecto
            if (string.IsNullOrEmpty(usuario.Role))
            {
                usuario.Role = "Cliente";
            }
            else if (usuario.Role != "Admin" && usuario.Role != "Vendedor" && usuario.Role != "Cliente")
            {
                // Validar que el rango sea uno de los permitidos
                return BadRequest("El rango debe ser 'Admin', 'Vendedor' o 'Cliente'.");
            }

            await _usuarioService.ModificarUsuario(usuario);
            return NoContent();
        }

        // Eliminar un usuario por ID
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
            if (usuario == null) return NotFound();
            await _usuarioService.EliminarUsuario(id);
            return NoContent();
        }
    }
}
