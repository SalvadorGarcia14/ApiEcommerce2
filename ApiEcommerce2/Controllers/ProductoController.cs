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

        // Obtener todos los productos
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productoService.ObtenerProductos();
            return Ok(productos);
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id) // Cambiado a int
        {
            var producto = await _productoService.ObtenerProductoPorId(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        // Crear un nuevo producto
        [HttpPost]
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

        // Modificar un producto existente por ID
        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProducto(int id, [FromBody] Producto producto)
        {
            if (producto.Id != id)
            {
                return BadRequest("El ID del producto en el cuerpo debe coincidir con el ID de la URL.");
            }

            var existingProducto = await _productoService.ObtenerProductoPorId(id);
            if (existingProducto == null)
            {
                return NotFound(); // Aquí se maneja el producto no encontrado sin lanzar error
            }

            // Actualizar el producto
            await _productoService.ModificarProducto(producto);
            return NoContent();
        }

        // Eliminar un producto por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id) // Cambiado a int
        {
            var existingProducto = await _productoService.ObtenerProductoPorId(id);
            if (existingProducto == null) return NotFound();

            await _productoService.EliminarProducto(id);
            return NoContent();
        }
    }
}