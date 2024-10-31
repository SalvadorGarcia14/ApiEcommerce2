using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrdenDto
    {
        public int Id { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? UsuarioEmail { get; set; }
        public decimal TotalPagar { get; set; }
        public List<DetalleOrdenDto> DetallesOrden { get; set; } = new List<DetalleOrdenDto>();
    }
}
