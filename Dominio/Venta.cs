using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int ID { get; set; }
        public int NumeroFactura { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }

        public List<DetalleVenta> detalleVentas { get; set; }
    }
}
