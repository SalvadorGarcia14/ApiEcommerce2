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

        public async Task<Producto> GetByIdAsync(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);
            return producto ?? throw new InvalidOperationException("Producto no encontrado."); // Lanzar excepción si no se encuentra
        }

        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null) // Verificar si el producto es nulo
            {
                throw new InvalidOperationException("Producto no encontrado."); // Lanzar excepción si no se encuentra
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }
    }
}