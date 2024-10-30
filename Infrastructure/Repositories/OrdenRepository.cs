using Domain.Entities.Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{

    public class OrdenRepository : IOrdenRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orden>> GetAllAsync()
        {
            // Obtener todas las órdenes junto con sus detalles
            return await _context.Ordenes
                .Include(o => o.DetallesOrden) // Incluye los detalles de la orden
                .ToListAsync();
        }

        public async Task<List<Orden>> GetByUsuarioEmailAsync(string email)
        {
            // Filtra las órdenes por el email del usuario (para clientes)
            return await _context.Ordenes
                .Where(o => o.UsuarioEmail == email)
                .Include(o => o.DetallesOrden) // Incluye los detalles de la orden
                .ToListAsync();
        }

        public async Task<Orden?> GetByIdAsync(int id)
        {
            // Busca una orden específica por ID, incluyendo los detalles
            return await _context.Ordenes
                .Include(o => o.DetallesOrden) // Incluye los detalles de la orden
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Orden orden)
        {
            // Agrega una nueva orden a la base de datos
            await _context.Ordenes.AddAsync(orden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Orden orden)
        {
            // Actualiza una orden existente
            _context.Ordenes.Update(orden);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Elimina una orden por ID
            var orden = await GetByIdAsync(id);
            if (orden != null)
            {
                _context.Ordenes.Remove(orden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
