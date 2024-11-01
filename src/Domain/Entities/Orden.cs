using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    namespace Domain.Entities
    {
        public class Orden
        {
            [Key]
            public int Id { get; set; }
            public string UsuarioNombre { get; set; }
            public string UsuarioEmail { get; set; } 
            public decimal TotalPagar { get; set; } 

            // Relación uno a muchos con DetalleOrden
            public ICollection<DetalleOrden> DetallesOrden { get; set; } = new List<DetalleOrden>();

            public Orden(string usuarioNombre, string usuarioEmail)
            {
                UsuarioNombre = usuarioNombre;
                UsuarioEmail = usuarioEmail;
            }

            public void CalcularTotal()
            {
                TotalPagar = DetallesOrden.Sum(d => d.PrecioUnitario * d.Cantidad);
            }

            // Método para agregar un detalle a la orden
            public void AgregarDetalle(DetalleOrden detalle)
            {
                DetallesOrden.Add(detalle);
                CalcularTotal(); 
            }
        }
    }
}