using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; } // Autoincremental
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rango { get; set; } // Admin, Vendedor, Cliente

        // Inicializa con cadena vacía
        public Usuario()
        {
            // El Id se asignará automáticamente por la base de datos
            Nombre = string.Empty;
            Apellido = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Rango = "Cliente"; // Valor predeterminado
        }
    }
}