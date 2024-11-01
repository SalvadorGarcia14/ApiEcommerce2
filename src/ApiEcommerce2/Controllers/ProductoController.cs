using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productoService.ObtenerProductos();
            return Ok(productos);
        }

        [HttpGet("nombre/{nombre}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductoPorNombre(string nombre)
        {
            var producto = await _productoService.ObtenerProductoPorNombre(nombre);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            // Validaciones
            if (string.IsNullOrEmpty(producto.Nombre))
            {
                return BadRequest("El nombre del producto no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(producto.Categoria))
            {
                return BadRequest("La categoría del producto no puede ser nulo.");
            }

            var categoriasPermitidas = new[] { "Motherboard", "Procesador", "MemoriaRam", "PlacaDeVideo" };
            if (!categoriasPermitidas.Contains(producto.Categoria))
            {
                return BadRequest("Categoría no válida. Las categorías permitidas son: Motherboard, Procesador, MemoriaRam, PlacaDeVideo.");
            }

            // Llamar al método de servicio para agregar o actualizar el producto
            await _productoService.AddOrUpdateAsync(producto);

            return Ok("Producto ya existente. Actualizado correctamente.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> ModificarProducto(int id, [FromBody] Producto producto)
        {
            if (producto.Id != id)
            {
                return BadRequest("El ID del producto en el cuerpo debe coincidir con el ID de la URL.");
            }

            var existingProducto = await _productoService.ObtenerProductoPorId(id);
            if (existingProducto == null)
            {
                return NotFound(); 
            }

            // Actualizar el producto
            await _productoService.ModificarProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Vendedor")]
        public async Task<IActionResult> EliminarProducto(int id) 
        {
            var existingProducto = await _productoService.ObtenerProductoPorId(id);
            if (existingProducto == null) return NotFound();

            await _productoService.EliminarProducto(id);
            return NoContent();
        }
    }
}