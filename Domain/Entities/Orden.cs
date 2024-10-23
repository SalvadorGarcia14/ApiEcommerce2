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
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleOrden> DetallesOrden { get; set; } = new();
        public double TotalCuenta { get; set; }

        public Orden()
        {
            DetallesOrden = new List<DetalleOrden>(); // Inicializa aquí
        }
    }
}
