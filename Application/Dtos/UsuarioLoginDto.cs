using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEcommerce2.DTOs
{
    public class UsuarioLoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }


        public UsuarioLoginDTO()
        {
            Email = string.Empty;
            Password = string.Empty;
            Role = string.Empty;
        }
    }

}