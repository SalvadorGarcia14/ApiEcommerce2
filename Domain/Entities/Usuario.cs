using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rango { get; set; } // Admin, Vendedor, Cliente

        // Inicializa con cadena vacía
        public Usuario()
        {
            Id = Guid.NewGuid();
            Nombre = string.Empty;
            Apellido = string.Empty; 
            Email = string.Empty; 
            Password = string.Empty; 
            Rango = string.Empty;
        }
    }
}
