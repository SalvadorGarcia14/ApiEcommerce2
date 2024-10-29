using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductoPorNombre(string nombre);

        Task<Producto> ObtenerProductoPorId(int id); // Cambiado a int
        Task AgregarProducto(Producto producto);
        Task ModificarProducto(Producto producto);
        Task EliminarProducto(int id); // Cambiado a int
        Task AddOrUpdateAsync(Producto producto);

    }
}