using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public string ProductoNombre { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
    }
}
