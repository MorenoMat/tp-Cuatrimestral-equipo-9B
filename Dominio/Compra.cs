using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal Total { get; set; }
        public Usuario Usuario { get; set; }
        public Proveedor Proveedor { get; set; }
        public List<DetalleCompra> DetalleCompras { get; set; }
    }
}
