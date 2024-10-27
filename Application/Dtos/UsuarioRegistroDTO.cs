using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class UsuarioRegistroDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rango { get; set; } = "Cliente"; // Por defecto Cliente

        public UsuarioRegistroDTO()
        {
            Nombre = string.Empty;
            Apellido = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        public bool IsAdminLoggedIn { get; set; }

    }
}
