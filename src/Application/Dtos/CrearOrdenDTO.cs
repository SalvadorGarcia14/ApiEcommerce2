using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;


namespace Application.Dtos
{
    public class CrearOrdenDTO
    {
        public string? NombreUsuario { get; set; }
        public string? EmailUsuario { get; set; }
        public List<DetalleOrdenDto> DetallesOrden { get; set; } = new List<DetalleOrdenDto>();
    }
}
