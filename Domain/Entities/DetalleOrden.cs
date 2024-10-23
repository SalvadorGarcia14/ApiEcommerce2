using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetalleOrden
    {
        [Key]
        public int Id { get; set; } // Autoincremental

        public int OrdenId { get; set; } // Clave foránea
        public Orden Orden { get; set; } = null!; // Relación con Orden
        public string NombreProducto { get; set; } = string.Empty; // Inicializa aquí
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
        public double SubTotal => Cantidad * PrecioUnitario;

        public DetalleOrden() { }
    }

}