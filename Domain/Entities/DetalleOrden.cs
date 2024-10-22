using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetalleOrden
    {
        public Guid Id { get; set; }  // Clave primaria
        public Guid OrdenId { get; set; }  // Clave foránea a Orden
        public Guid ProductoId { get; set; }  // Parte de la clave compuesta
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double SubTotal => Cantidad * PrecioUnitario;

        public DetalleOrden()
        {
            NombreProducto = string.Empty;
        }
    }
}