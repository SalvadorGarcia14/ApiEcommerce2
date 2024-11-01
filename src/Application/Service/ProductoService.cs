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

        public async Task<Producto> ObtenerProductoPorNombre(string nombre)
        {
            return await _productoRepository.GetByNombreAsync(nombre);
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            return await _productoRepository.GetByIdAsync(id);
        }

        public async Task AgregarProducto(Producto producto)
        {
            await _productoRepository.AddAsync(producto);
        }
        public async Task AddOrUpdateAsync(Producto producto)
        {
            await _productoRepository.AddOrUpdateAsync(producto);
        }

        public async Task ModificarProducto(Producto producto)
        {
            await _productoRepository.UpdateAsync(producto);
        }

        public async Task EliminarProducto(int id) 
        {
            await _productoRepository.DeleteAsync(id);
        }
    }
}