using Domain.Entities;
using Domain.Entities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrdenService
    {
        Task<List<Orden>> ObtenerOrdenesPorUsuario(string email, string role);
        Task<Orden> ObtenerOrdenPorId(int id);
        Task CrearOrden(Orden orden);
        Task ModificarOrden(Orden orden);
        Task EliminarOrden(int id);
    }
}