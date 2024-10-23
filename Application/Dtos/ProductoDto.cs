using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class ProductoDto
    {

        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public string? Imagen { get; set; }
        public bool Status { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
    }
}