using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var producto = await _context.Productos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            return producto;
        }
        public async Task AddAsync(Producto producto)
        {
            // No asignes el Id manualmente aquí
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync(); // Esto guardará el producto y generará el Id automáticamente
        }

        public async Task UpdateAsync(Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(producto.Id);

            if (productoExistente == null)
            {
                throw new InvalidOperationException("Producto no encontrado.");
            }

            // Actualiza los valores del producto existente con los del nuevo producto
            _context.Entry(productoExistente).CurrentValues.SetValues(producto);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) // Cambiado a int
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                throw new InvalidOperationException("Producto no encontrado."); // Lanzar excepción si no se encuentra
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
    }
}