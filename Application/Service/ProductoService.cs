using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _productoRepository.GetAllAsync();
        }

        public async Task<Producto> ObtenerProductoPorId(int id) // Cambiado a int
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        public async Task AgregarProducto(Producto producto)
        {
            await _productoRepository.AddAsync(producto);
        }

        public async Task ModificarProducto(Producto producto)
        {
            await _productoRepository.UpdateAsync(producto);
        }

        public async Task EliminarProducto(int id) // Cambiado a int
        {
            await _productoRepository.DeleteAsync(id);
        }
    }
}