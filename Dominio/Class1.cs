using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public int ID { get; set; }
        public int IDVenta { get; set; }
        public int IDProducto { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public string ProductoNombre { get; set; }
    }
}
