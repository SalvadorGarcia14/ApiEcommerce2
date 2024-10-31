using Domain.Entities;
using Domain.Entities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces
{
    public interface IOrdenRepository
    {
        Task<List<Orden>> GetAllAsync();
        Task<List<Orden>> GetByUsuarioEmailAsync(string email); // Para clientes
        Task<Orden?> GetByIdAsync(int id);
        Task AddAsync(Orden orden);
        Task UpdateAsync(Orden orden); 
        Task DeleteAsync(int id);
        Task<Orden> GetOrdenByIdAsync(int id);
        Task SaveChangesAsync();
    }
}