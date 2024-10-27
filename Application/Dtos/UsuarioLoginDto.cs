using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEcommerce2.DTOs
{
    public class UsuarioLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UsuarioLoginDTO()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }

}