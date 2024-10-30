using Application.Dtos;
using Domain.Entities;
using Domain.Entities.Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class OrdenService : IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public OrdenService(IOrdenRepository ordenRepository, IUsuarioRepository usuarioRepository)
        {
            _ordenRepository = ordenRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Orden>> ObtenerOrdenesPorUsuario(string email, string role)
        {
            // Los administradores y vendedores pueden ver todas las órdenes
            if (role == "Admin" || role == "Vendedor")
            {
                return await _ordenRepository.GetAllAsync();
            }
            // Los clientes solo ven sus propias órdenes
            return await _ordenRepository.GetByUsuarioEmailAsync(email);
        }

        public async Task<Orden> ObtenerOrdenPorId(int id)
        {
            var orden = await _ordenRepository.GetByIdAsync(id);
            if (orden == null)
            {
                throw new ArgumentNullException($"No se encontró una orden con el ID {id}");
            }
            return orden;
        }

        public async Task CrearOrden(Orden orden)
        {
            orden.CalcularTotal(); // Calcular el total a pagar antes de guardar
            await _ordenRepository.AddAsync(orden);
        }

        public async Task ModificarOrden(Orden orden)
        {
            orden.CalcularTotal();
            await _ordenRepository.UpdateAsync(orden);
        }

        public async Task EliminarOrden(int id)
        {
            await _ordenRepository.DeleteAsync(id);
        }
    }
}