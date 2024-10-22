using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Orden
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public List<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>(); // Inicialización predeterminada
        public double TotalCuenta { get; set; }


    }
}
