using Domain.Entities;
using Domain.Interfaces;
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
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productoService.ObtenerProductos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(Guid id)
        {
            var producto = await _productoService.ObtenerProductoPorId(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] Producto producto)
        {
            await _productoService.AgregarProducto(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProducto(Guid id, [FromBody] Producto producto)
        {
            var existingProducto = await _productoService.ObtenerProductoPorId(id);
            if (existingProducto == null) return NotFound();

            producto.Id = id;
            await _productoService.ModificarProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(Guid id)
        {
            await _productoService.EliminarProducto(id);
            return NoContent();
        }
    }
}
