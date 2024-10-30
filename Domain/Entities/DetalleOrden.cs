using Domain.Entities.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DetalleOrden
    {
        [Key]
        public int Id { get; set; }
        public string ProductoNombre { get; set; }
        public string Categoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }

        // Relación con Orden
        [ForeignKey("OrdenId")]
        public int OrdenId { get; set; }
        public Orden? Orden { get; set; }  // Cambiado para permitir nulos

        public DetalleOrden(string productoNombre, string categoria, decimal precioUnitario, int cantidad)
        {
            ProductoNombre = productoNombre;
            Categoria = categoria;
            PrecioUnitario = precioUnitario;
            Cantidad = cantidad;
        }
    }
}