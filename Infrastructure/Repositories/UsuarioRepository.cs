using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByNombreAsync(string nombre)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre);
        }

        public async Task<Usuario?> GetByIdAsync(int id) // Coincide con la interfaz
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario; // Puede ser null
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            var existingUsuario = await _context.Usuarios.FindAsync(usuario.Id);

            if (existingUsuario != null)
            {
                _context.Entry(existingUsuario).State = EntityState.Detached; // Desconecta la entidad rastreada
            }

            _context.Usuarios.Update(usuario); // Adjunta la nueva entidad
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountClientesAsync()
        {
            return await _context.Usuarios.CountAsync(u => u.Role == "Cliente");
        }


    }
}