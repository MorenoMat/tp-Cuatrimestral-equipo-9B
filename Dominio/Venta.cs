using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int IdVenta{ get; set; }
        public int NumeroFactura { get; set; }
        public bool EstadoVenta { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; } // tener el usuario que hizo la venta
        public decimal Total { get; set; }
       

        public List<DetalleVenta> detalleVentas { get; set; }
    }
}
// tener unos 2 roles fijos