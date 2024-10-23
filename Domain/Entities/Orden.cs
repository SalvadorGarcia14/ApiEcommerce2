using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Orden
    {
        [Key]

        public int Id { get; set; } // Autoincremental
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>(); // Inicialización predeterminada
        public double TotalCuenta { get; set; }


    }
}
