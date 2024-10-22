using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string? Imagen { get; set; }
        public bool Status { get; set; }
        public string? Descripcion { get; set; }

        public Producto(string nombre)
        {
            Id = Guid.NewGuid(); // Generar un nuevo ID
            Nombre = nombre;
            
        }

        public void ActualizarStock(int cantidad)
        {
            Cantidad = cantidad;
            Status = cantidad > 0;
        }
    }
}
