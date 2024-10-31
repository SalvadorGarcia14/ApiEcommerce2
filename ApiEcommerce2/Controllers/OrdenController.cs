using Application.Dtos;
using Domain.Entities.Domain.Entities;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenService _ordenService;
        private readonly IUsuarioService _usuarioService;
        private readonly IOrdenRepository _ordenRepository; // Asegúrate de que esto esté presente


        public OrdenController(IOrdenService ordenService, IUsuarioService usuarioService, IOrdenRepository ordenRepository)
        {
            _ordenService = ordenService;
            _usuarioService = usuarioService;
            _ordenRepository = ordenRepository;

        }

        [HttpGet]
        [Authorize(Roles = "Admin,Vendedor,Cliente")]
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
        [Authorize(Roles = "Admin,Vendedor")]
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
            if (orden == null)
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
                }).ToList() ?? new List<DetalleOrdenDto>()
            };
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Vendedor")] // Restringe el acceso a admin y vendedor
        public async Task<IActionResult> UpdateOrden(int id, [FromBody] Orden orden)
        {
            if (id != orden.Id)
            {
                return BadRequest();
            }

            // Obtén la orden existente de la base de datos
            var ordenExistente = await _ordenRepository.GetOrdenByIdAsync(id);
            if (ordenExistente == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades de la orden existente
            ordenExistente.UsuarioNombre = orden.UsuarioNombre;
            ordenExistente.UsuarioEmail = orden.UsuarioEmail;

            // Actualiza detalles de la orden y calcula el nuevo total
            decimal nuevoTotalPagar = 0;

            foreach (var detalle in orden.DetallesOrden)
            {
                var detalleExistente = ordenExistente.DetallesOrden
                    .FirstOrDefault(d => d.Id == detalle.Id);
                if (detalleExistente != null)
                {
                    detalleExistente.ProductoNombre = detalle.ProductoNombre;
                    detalleExistente.Categoria = detalle.Categoria;
                    detalleExistente.PrecioUnitario = detalle.PrecioUnitario;
                    detalleExistente.Cantidad = detalle.Cantidad;

                    nuevoTotalPagar += detalleExistente.PrecioUnitario * detalleExistente.Cantidad;
                }
            }

            ordenExistente.TotalPagar = nuevoTotalPagar;

            // Guarda los cambios en la base de datos usando el método correcto
            await _ordenRepository.UpdateAsync(ordenExistente); // Cambiado a UpdateAsync
            await _ordenRepository.SaveChangesAsync();

            return NoContent();
        }

        // Método para eliminar una orden
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Vendedor")] // Restringe el acceso a admin y vendedor
        public async Task<IActionResult> DeleteOrden(int id)
        {
            var existingOrden = await _ordenRepository.GetByIdAsync(id);
            if (existingOrden == null)
            {
                return NotFound("Orden no encontrada.");
            }

            await _ordenRepository.DeleteAsync(id);
            return NoContent(); 
        }

    }
}