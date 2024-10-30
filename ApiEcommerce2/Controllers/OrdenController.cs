using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Application.Dtos;
using Domain.Entities.Domain.Entities;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        private readonly IUsuarioService _usuarioService;

        public OrdenController(IOrdenService ordenService, IUsuarioService usuarioService)
        {
            _ordenService = ordenService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrdenes()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("Usuario no autenticado.");
            }

            var usuario = await _usuarioService.ObtenerPorEmailAsync(userName);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var ordenes = await _ordenService.ObtenerOrdenesPorUsuario(usuario.Email, usuario.Role);
            return Ok(ordenes.Select(o => MapToDto(o)));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Vendedor,Cliente")]
        public async Task<IActionResult> CrearOrden(OrdenDto ordenDto)
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized("Usuario no autenticado.");
            }

            var usuario = await _usuarioService.ObtenerPorEmailAsync(userName);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var orden = new Orden(usuario.Nombre, usuario.Email);
            orden.DetallesOrden = ordenDto.DetallesOrden
                .Select(d =>
                {
                    // Verificar que los campos no sean nulos
                    var productoNombre = d.ProductoNombre ?? throw new ArgumentNullException(nameof(d.ProductoNombre));
                    var categoria = d.Categoria ?? throw new ArgumentNullException(nameof(d.Categoria));

                    return new DetalleOrden(productoNombre, categoria, d.PrecioUnitario, d.Cantidad);
                })
                .ToList();

            await _ordenService.CrearOrden(orden);
            return CreatedAtAction(nameof(GetOrdenes), new { id = orden.Id }, MapToDto(orden));
        }
private OrdenDto MapToDto(Orden orden)
        {
            if (orden == null) // Comprobación nula
            {
                throw new ArgumentNullException(nameof(orden), "El objeto Orden no puede ser nulo.");
            }

            return new OrdenDto
            {
                Id = orden.Id,
                UsuarioNombre = orden.UsuarioNombre,
                UsuarioEmail = orden.UsuarioEmail,
                TotalPagar = orden.TotalPagar,
                DetallesOrden = orden.DetallesOrden?.Select(d => new DetalleOrdenDto
                {
                    Id = d.Id,
                    ProductoNombre = d.ProductoNombre,
                    Categoria = d.Categoria,
                    PrecioUnitario = d.PrecioUnitario,
                    Cantidad = d.Cantidad
                }).ToList() ?? new List<DetalleOrdenDto>() // Manejar caso nulo
            };
        }
    }
}