using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DetalleOrdenDto
    {
        public int Id { get; set; }
        public string? ProductoNombre { get; set; }
        public string? Categoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
    }
}
