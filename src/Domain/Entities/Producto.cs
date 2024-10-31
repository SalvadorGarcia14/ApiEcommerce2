using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Producto
    {
        [Key]
        public int Id { get; set; } // El Id debe ser manejado por la base de datos
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string? Imagen { get; set; }
        public bool Status { get; set; }
        public string? Descripcion { get; set; }
        public string Categoria { get; set; }

        // Constructor que acepta Nombre, Precio, Cantidad, Imagen, Status, Descripcion y Categoria
        public Producto(string nombre, decimal precio, int cantidad, string? imagen, bool status, string? descripcion, string categoria)
        {
            Nombre = nombre;
            Precio = precio;
            Cantidad = cantidad;
            Imagen = imagen;
            Status = cantidad > 0; // Define Status basado en Cantidad
            Descripcion = descripcion;
            Categoria = categoria;
        }

        public void ActualizarStock(int cantidad)
        {
            Cantidad = cantidad;
            Status = cantidad > 0;
        }
    }
}